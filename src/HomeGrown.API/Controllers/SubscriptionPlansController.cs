using System.Security.Claims;
using HomeGrown.Core.Application.DTOs.Subscriptions;
using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeGrown.API.Controllers;

[ApiController]
[Route("api")]
public class SubscriptionPlansController(IUnitOfWork uow) : ControllerBase
{
    /// <summary>POST /api/farms/{farmId}/subscription-plans — farmer creates a plan</summary>
    [HttpPost("farms/{farmId:guid}/subscription-plans")]
    [Authorize(Roles = "Farmer")]
    public async Task<IActionResult> Create(Guid farmId, [FromBody] CreateSubscriptionPlanRequest request)
    {
        var farm = await uow.Farms.GetByIdAsync(farmId);
        if (farm is null) return NotFound(new { error = "Farm not found." });
        if (farm.OwnerId != GetUserId()) return Forbid();

        var plan = new SubscriptionPlan
        {
            FarmId = farmId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Frequency = request.Frequency,
            MaxSubscribers = request.MaxSubscribers
        };

        await uow.SubscriptionPlans.AddAsync(plan);
        await uow.SaveChangesAsync();

        // Re-fetch with Farm so FarmName is available for the DTO
        var planWithFarm = await uow.SubscriptionPlans.GetByIdWithFarmAsync(plan.Id);
        return CreatedAtAction(nameof(GetByFarm), new { farmId }, MapToDto(planWithFarm!));
    }

    /// <summary>GET /api/farms/{farmId}/subscription-plans — public</summary>
    [HttpGet("farms/{farmId:guid}/subscription-plans")]
    public async Task<IActionResult> GetByFarm(Guid farmId)
    {
        var farm = await uow.Farms.GetByIdAsync(farmId);
        if (farm is null) return NotFound(new { error = "Farm not found." });

        var plans = await uow.SubscriptionPlans.GetActivePlansByFarmIdAsync(farmId);
        return Ok(plans.Select(sp => new SubscriptionPlanDto(
            sp.Id, sp.FarmId, farm.Name, sp.Name, sp.Description,
            sp.Price, sp.Frequency, sp.MaxSubscribers, sp.IsActive,
            sp.CreatedAt, sp.UpdatedAt)));
    }

    /// <summary>PUT /api/subscription-plans/{id} — farmer updates own plan</summary>
    [HttpPut("subscription-plans/{id:guid}")]
    [Authorize(Roles = "Farmer")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSubscriptionPlanRequest request)
    {
        var plan = await uow.SubscriptionPlans.GetByIdWithFarmAsync(id);
        if (plan is null) return NotFound();
        if (plan.Farm.OwnerId != GetUserId()) return Forbid();

        if (request.Name is not null) plan.Name = request.Name;
        if (request.Description is not null) plan.Description = request.Description;
        if (request.Price.HasValue) plan.Price = request.Price.Value;
        if (request.Frequency.HasValue) plan.Frequency = request.Frequency.Value;
        if (request.MaxSubscribers.HasValue) plan.MaxSubscribers = request.MaxSubscribers.Value;
        if (request.IsActive.HasValue) plan.IsActive = request.IsActive.Value;
        plan.UpdatedAt = DateTime.UtcNow;

        uow.SubscriptionPlans.Update(plan);
        await uow.SaveChangesAsync();

        return Ok(MapToDto(plan));
    }

    /// <summary>DELETE /api/subscription-plans/{id} — soft delete (IsActive = false)</summary>
    [HttpDelete("subscription-plans/{id:guid}")]
    [Authorize(Roles = "Farmer")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var plan = await uow.SubscriptionPlans.GetByIdWithFarmAsync(id);
        if (plan is null) return NotFound();
        if (plan.Farm.OwnerId != GetUserId()) return Forbid();

        plan.IsActive = false;
        plan.UpdatedAt = DateTime.UtcNow;

        uow.SubscriptionPlans.Update(plan);
        await uow.SaveChangesAsync();

        return NoContent();
    }

    // ─── Helpers ──────────────────────────────────────────────────────────────

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException());

    private static SubscriptionPlanDto MapToDto(SubscriptionPlan sp) => new(
        sp.Id, sp.FarmId, sp.Farm.Name, sp.Name, sp.Description,
        sp.Price, sp.Frequency, sp.MaxSubscribers, sp.IsActive,
        sp.CreatedAt, sp.UpdatedAt);
}
