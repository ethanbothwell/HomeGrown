using System.Security.Claims;
using HomeGrown.Core.Application.DTOs.Farms;
using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeGrown.API.Controllers;

[ApiController]
[Route("api/farms")]
public class FarmsController(IUnitOfWork uow) : ControllerBase
{
    /// <summary>GET /api/farms — public</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? category, [FromQuery] string? state, [FromQuery] string? query)
    {
        var farms = await uow.Farms.SearchAsync(category, state, query);
        return Ok(farms.Select(MapToDto));
    }

    /// <summary>GET /api/farms/{id} — public</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var farm = await uow.Farms.GetByIdWithDetailsAsync(id);
        return farm is null ? NotFound() : Ok(MapToDto(farm));
    }

    /// <summary>POST /api/farms — farmer only</summary>
    [HttpPost]
    [Authorize(Roles = "Farmer")]
    public async Task<IActionResult> Create([FromBody] CreateFarmRequest request)
    {
        var userId = GetUserId();

        if (await uow.Farms.GetByOwnerIdAsync(userId) is not null)
            return Conflict(new { error = "You already have a farm registered." });

        var farm = new Farm
        {
            OwnerId = userId,
            Name = request.Name,
            Bio = request.Bio,
            Philosophy = request.Philosophy,
            Location = request.Location,
            City = request.City,
            State = request.State,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            ImageUrl = request.ImageUrl
        };

        await uow.Farms.AddAsync(farm);
        await uow.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = farm.Id }, MapToDto(farm));
    }

    /// <summary>PUT /api/farms/{id} — farmer (own farm) or admin</summary>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Farmer,Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFarmRequest request)
    {
        var farm = await uow.Farms.GetByIdAsync(id);
        if (farm is null) return NotFound();

        // Farmers can only edit their own farm
        if (!IsAdmin() && farm.OwnerId != GetUserId())
            return Forbid();

        if (request.Name is not null) farm.Name = request.Name;
        if (request.Bio is not null) farm.Bio = request.Bio;
        if (request.Philosophy is not null) farm.Philosophy = request.Philosophy;
        if (request.Location is not null) farm.Location = request.Location;
        if (request.City is not null) farm.City = request.City;
        if (request.State is not null) farm.State = request.State;
        if (request.Latitude.HasValue) farm.Latitude = request.Latitude;
        if (request.Longitude.HasValue) farm.Longitude = request.Longitude;
        if (request.ImageUrl is not null) farm.ImageUrl = request.ImageUrl;
        farm.UpdatedAt = DateTime.UtcNow;

        uow.Farms.Update(farm);
        await uow.SaveChangesAsync();

        return Ok(MapToDto(farm));
    }

    // ─── Helpers ──────────────────────────────────────────────────────────────

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException());

    private bool IsAdmin() =>
        User.IsInRole("Admin");

    private static FarmDto MapToDto(Farm f) => new(
        f.Id, f.Name, f.Bio, f.Philosophy, f.Location, f.City, f.State,
        f.Latitude, f.Longitude, f.ImageUrl, f.Rating, f.ReviewCount,
        f.Practices.Select(p => p.Name).ToList(),
        f.Owner?.Name ?? string.Empty,
        f.Owner?.ProfileImageUrl
    );
}
