using HomeGrown.API.Services;
using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HomeGrown.API.Controllers;

public record NewsletterSubscribeRequest([Required, EmailAddress] string Email);

[ApiController]
[Route("api/subscribe")]
public class SubscribersController(IUnitOfWork uow, IEmailService email) : ControllerBase
{
    /// <summary>POST /api/subscribe — public</summary>
    [HttpPost]
    public async Task<IActionResult> Subscribe([FromBody] NewsletterSubscribeRequest request)
    {
        var addr = request.Email.ToLower();

        if (await uow.Subscribers.EmailExistsAsync(addr))
            return Ok(new { success = true, duplicate = true });

        await uow.Subscribers.AddAsync(new Subscriber { Email = addr });
        await uow.SaveChangesAsync();

        // Fire-and-forget — don't block the response on email delivery
        _ = email.SendWaitlistConfirmationAsync(addr);

        return Ok(new { success = true, duplicate = false });
    }
}
