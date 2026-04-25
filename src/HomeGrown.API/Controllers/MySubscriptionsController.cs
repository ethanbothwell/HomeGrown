using System.Security.Claims;
using HomeGrown.Core.Application.DTOs.Subscriptions;
using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Enums;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeGrown.API.Controllers;

[ApiController]
[Route("api/my/subscriptions")]
[Authorize]
public class MySubscriptionsController(IUnitOfWork uow) : ControllerBase
{
    /// <summary>POST /api/subscription-plans/{id}/subscribe — buyer subscribes to a plan</summary>
    [HttpPost("/api/subscription-plans/{id:guid}/subscribe")]
    [Authorize(Roles = "Buyer")]
    public async Task<IActionResult> Subscribe(Guid id, [FromBody] SubscribeRequest request)
    {
        var plan = await uow.SubscriptionPlans.GetByIdWithFarmAsync(id);
        if (plan is null) return NotFound(new { error = "Subscription plan not found." });
        if (!plan.IsActive) return BadRequest(new { error = "This plan is not currently accepting new subscribers." });

        if (plan.MaxSubscribers.HasValue)
        {
            var count = await uow.FarmSubscriptions.GetActiveSubscriberCountAsync(id);
            if (count >= plan.MaxSubscribers.Value)
                return Conflict(new { error = "This plan is full." });
        }

        var nextDelivery = plan.Frequency switch
        {
            SubscriptionFrequency.Weekly => DateTime.UtcNow.AddDays(7),
            SubscriptionFrequency.Biweekly => DateTime.UtcNow.AddDays(14),
            SubscriptionFrequency.Monthly => DateTime.UtcNow.AddMonths(1),
            _ => DateTime.UtcNow.AddDays(7)
        };

        var subscription = new FarmSubscription
        {
            BuyerId = GetUserId(),
            SubscriptionPlanId = id,
            NextDeliveryDate = nextDelivery,
            ShippingAddress = request.ShippingAddress,
            Notes = request.Notes
        };

        await uow.FarmSubscriptions.AddAsync(subscription);
        await uow.SaveChangesAsync();

        // Re-fetch with plan to build full DTO
        var subWithPlan = await uow.FarmSubscriptions.GetByIdWithPlanAsync(subscription.Id);
        return CreatedAtAction(nameof(GetMine), null, MapToDto(subWithPlan!));
    }

    /// <summary>GET /api/my/subscriptions — buyer lists their subscriptions</summary>
    [HttpGet]
    public async Task<IActionResult> GetMine()
    {
        var subs = await uow.FarmSubscriptions.GetByBuyerIdAsync(GetUserId());
        return Ok(subs.Select(MapToDto));
    }

    /// <summary>POST /api/my/subscriptions/{id}/pause</summary>
    [HttpPost("{id:guid}/pause")]
    public async Task<IActionResult> Pause(Guid id)
    {
        var sub = await uow.FarmSubscriptions.GetByIdWithPlanAsync(id);
        if (sub is null) return NotFound();
        if (sub.BuyerId != GetUserId()) return Forbid();
        if (sub.Status != SubscriptionStatus.Active)
            return BadRequest(new { error = "Only active subscriptions can be paused." });

        sub.Status = SubscriptionStatus.Paused;
        sub.UpdatedAt = DateTime.UtcNow;

        uow.FarmSubscriptions.Update(sub);
        await uow.SaveChangesAsync();

        return Ok(MapToDto(sub));
    }

    /// <summary>POST /api/my/subscriptions/{id}/cancel</summary>
    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var sub = await uow.FarmSubscriptions.GetByIdWithPlanAsync(id);
        if (sub is null) return NotFound();
        if (sub.BuyerId != GetUserId()) return Forbid();
        if (sub.Status == SubscriptionStatus.Cancelled)
            return BadRequest(new { error = "This subscription is already cancelled." });

        sub.Status = SubscriptionStatus.Cancelled;
        sub.UpdatedAt = DateTime.UtcNow;

        uow.FarmSubscriptions.Update(sub);
        await uow.SaveChangesAsync();

        return Ok(MapToDto(sub));
    }

    // ─── Helpers ──────────────────────────────────────────────────────────────

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException());

    private static FarmSubscriptionDto MapToDto(FarmSubscription fs) => new(
        fs.Id,
        fs.SubscriptionPlanId,
        fs.Plan.Name,
        fs.Plan.Farm.Name,
        fs.Plan.Price,
        fs.Plan.Frequency,
        fs.Status,
        fs.StartDate,
        fs.NextDeliveryDate,
        fs.ShippingAddress,
        fs.Notes,
        fs.CreatedAt,
        fs.UpdatedAt);
}
