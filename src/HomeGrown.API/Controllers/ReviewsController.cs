using System.Security.Claims;
using HomeGrown.Core.Application.DTOs.Reviews;
using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeGrown.API.Controllers;

[ApiController]
[Route("api/farms/{farmId:guid}/reviews")]
public class ReviewsController(IUnitOfWork uow) : ControllerBase
{
    /// <summary>GET /api/farms/{farmId}/reviews — public</summary>
    [HttpGet]
    public async Task<IActionResult> GetByFarm(Guid farmId)
    {
        var reviews = await uow.Reviews.GetByFarmIdAsync(farmId);
        return Ok(reviews.Select(MapToDto));
    }

    /// <summary>POST /api/farms/{farmId}/reviews — authenticated buyers only</summary>
    [HttpPost]
    [Authorize(Roles = "Buyer")]
    public async Task<IActionResult> Create(Guid farmId, [FromBody] CreateReviewRequest request)
    {
        if (await uow.Farms.GetByIdAsync(farmId) is null)
            return NotFound(new { error = "Farm not found." });

        var userId = GetUserId();

        if (await uow.Reviews.UserHasReviewedFarmAsync(userId, farmId))
            return Conflict(new { error = "You have already reviewed this farm." });

        var review = new Review
        {
            FarmId = farmId,
            UserId = userId,
            Rating = request.Rating,
            Text = request.Text
        };

        await uow.Reviews.AddAsync(review);

        // Recalculate farm rating
        var farm = await uow.Farms.GetByIdAsync(farmId);
        if (farm is not null)
        {
            var allReviews = (await uow.Reviews.GetByFarmIdAsync(farmId)).ToList();
            farm.ReviewCount = allReviews.Count + 1;
            farm.Rating = Math.Round(
                (allReviews.Sum(r => r.Rating) + request.Rating) / (double)farm.ReviewCount, 1);
            uow.Farms.Update(farm);
        }

        await uow.SaveChangesAsync();
        return Created(string.Empty, MapToDto(review));
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException());

    private static ReviewDto MapToDto(Review r) => new(
        r.Id, r.FarmId,
        r.User?.Name ?? "Anonymous",
        r.User?.ProfileImageUrl,
        r.Rating, r.Text, r.CreatedAt
    );
}
