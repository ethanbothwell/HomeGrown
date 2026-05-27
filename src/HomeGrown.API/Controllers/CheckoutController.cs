using System.Security.Claims;
using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Enums;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;

namespace HomeGrown.API.Controllers;

public record CreateCheckoutSessionRequest(
    Guid PlanId,
    string SuccessUrl,
    string CancelUrl,
    string? ShippingAddress,
    string? Notes
);

[ApiController]
public class CheckoutController(IUnitOfWork uow, IConfiguration config) : ControllerBase
{
    /// <summary>POST /api/checkout/session — buyer initiates payment</summary>
    [HttpPost("api/checkout/session")]
    [Authorize(Roles = "Buyer")]
    public async Task<IActionResult> CreateSession([FromBody] CreateCheckoutSessionRequest request)
    {
        var buyerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var plan = await uow.SubscriptionPlans.GetByIdWithFarmAsync(request.PlanId);
        if (plan is null || !plan.IsActive)
            return NotFound("Subscription plan not found or inactive.");

        if (plan.MaxSubscribers.HasValue)
        {
            var count = await uow.FarmSubscriptions.GetActiveSubscriberCountAsync(plan.Id);
            if (count >= plan.MaxSubscribers.Value)
                return BadRequest("This plan is at full capacity.");
        }

        var (interval, intervalCount) = plan.Frequency switch
        {
            SubscriptionFrequency.Weekly   => ("week",  1L),
            SubscriptionFrequency.Biweekly => ("week",  2L),
            SubscriptionFrequency.Monthly  => ("month", 1L),
            _                              => ("month", 1L)
        };

        var options = new SessionCreateOptions
        {
            Mode = "subscription",
            LineItems =
            [
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = (long)(plan.Price * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"{plan.Farm.Name} — {plan.Name}",
                            Description = plan.Description
                        },
                        Recurring = new SessionLineItemPriceDataRecurringOptions
                        {
                            Interval = interval,
                            IntervalCount = intervalCount
                        }
                    },
                    Quantity = 1
                }
            ],
            Metadata = new Dictionary<string, string>
            {
                ["planId"]          = plan.Id.ToString(),
                ["buyerId"]         = buyerId.ToString(),
                ["shippingAddress"] = request.ShippingAddress ?? "",
                ["notes"]           = request.Notes ?? ""
            },
            SuccessUrl = request.SuccessUrl,
            CancelUrl  = request.CancelUrl
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return Ok(new { url = session.Url });
    }

    /// <summary>POST /api/stripe/webhook — Stripe calls this after payment</summary>
    [HttpPost("api/stripe/webhook")]
    [AllowAnonymous]
    public async Task<IActionResult> Webhook()
    {
        var json = await new StreamReader(Request.Body).ReadToEndAsync();
        var webhookSecret = config["Stripe:WebhookSecret"];

        Event stripeEvent;
        try
        {
            stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                webhookSecret
            );
        }
        catch (StripeException)
        {
            return BadRequest();
        }

        if (stripeEvent.Type == EventTypes.CheckoutSessionCompleted)
        {
            var session = (Session)stripeEvent.Data.Object;

            if (!session.Metadata.TryGetValue("planId", out var planIdStr) ||
                !session.Metadata.TryGetValue("buyerId", out var buyerIdStr))
                return Ok();

            var planId  = Guid.Parse(planIdStr);
            var buyerId = Guid.Parse(buyerIdStr);
            session.Metadata.TryGetValue("shippingAddress", out var shippingAddress);
            session.Metadata.TryGetValue("notes", out var notes);

            var plan = await uow.SubscriptionPlans.GetByIdAsync(planId);
            if (plan is null) return Ok();

            var nextDelivery = plan.Frequency switch
            {
                SubscriptionFrequency.Weekly   => DateTime.UtcNow.AddDays(7),
                SubscriptionFrequency.Biweekly => DateTime.UtcNow.AddDays(14),
                SubscriptionFrequency.Monthly  => DateTime.UtcNow.AddMonths(1),
                _                              => DateTime.UtcNow.AddMonths(1)
            };

            await uow.FarmSubscriptions.AddAsync(new FarmSubscription
            {
                BuyerId             = buyerId,
                SubscriptionPlanId  = planId,
                Status              = SubscriptionStatus.Active,
                StartDate           = DateTime.UtcNow,
                NextDeliveryDate    = nextDelivery,
                ShippingAddress     = string.IsNullOrEmpty(shippingAddress) ? null : shippingAddress,
                Notes               = string.IsNullOrEmpty(notes) ? null : notes
            });
            await uow.SaveChangesAsync();
        }

        return Ok();
    }
}
