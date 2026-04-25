using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HomeGrown.API.Controllers;

public record NewsletterSubscribeRequest([Required, EmailAddress] string Email);

[ApiController]
[Route("api/subscribe")]
public class SubscribersController(IUnitOfWork uow) : ControllerBase
{

    /// <summary>POST /api/subscribe — public</summary>
    [HttpPost]
    public async Task<IActionResult> Subscribe([FromBody] NewsletterSubscribeRequest request)
    {
        var email = request.Email.ToLower();

        if (await uow.Subscribers.EmailExistsAsync(email))
            return Ok(new { success = true, duplicate = true });

        await uow.Subscribers.AddAsync(new Subscriber { Email = email });
        await uow.SaveChangesAsync();

        return Ok(new { success = true, duplicate = false });
    }
}
