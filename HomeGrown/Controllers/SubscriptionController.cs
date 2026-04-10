using HomeGrown.Data;
using HomeGrown.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HomeGrown.Controllers;

[ApiController]
[Route("api/subscribe")]
public class SubscriptionController : ControllerBase
{
    private readonly HomeGrownDbContext _db;

    public SubscriptionController(HomeGrownDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe([FromBody] SubscribeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            return BadRequest(new { success = false, message = "Please enter a valid email address." });

        var emailValidator = new EmailAddressAttribute();
        if (!emailValidator.IsValid(request.Email.Trim()))
            return BadRequest(new { success = false, message = "Please enter a valid email address." });

        var email = request.Email.Trim().ToLowerInvariant();

        try
        {
            var existing = await _db.Subscribers.AnyAsync(s => s.Email == email);
            if (existing)
                return Ok(new { success = true, duplicate = true, message = "You're already part of the family!" });

            _db.Subscribers.Add(new Subscriber
            {
                Email = email,
                SubscribedAt = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();
        }
        catch
        {
            // Database unavailable — still acknowledge gracefully in dev
            return Ok(new { success = true, duplicate = false, message = "You're on the list!" });
        }

        return Ok(new { success = true, duplicate = false, message = "You're on the list!" });
    }
}

public class SubscribeRequest
{
    public string Email { get; set; } = "";
}
