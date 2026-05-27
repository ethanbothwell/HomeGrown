using System.Security.Claims;
using HomeGrown.Data;
using HomeGrown.Models;
using HomeGrown.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeGrown.Controllers;

[ApiController]
[Route("api/farms")]
public class FarmsApiController : ControllerBase
{
    private readonly HomeGrownDbContext _db;

    public FarmsApiController(HomeGrownDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetFarms([FromQuery] string? category = null)
    {
        var farms = SonomaFarmsService.Farms.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(category) && category != "All")
            farms = farms.Where(f => f.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

        var result = farms.Select(f => new
        {
            f.Id,
            f.Name,
            f.Description,
            f.Latitude,
            f.Longitude,
            f.Category,
            f.ImageUrl,
            f.Rating,
            f.ProductCount,
            f.IsOpen,
            f.DistanceMi
        });

        return Ok(result);
    }

    [Authorize]
    [HttpPost("{farmId}/follow")]
    public async Task<IActionResult> Follow(int farmId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var exists = await _db.UserFollowedFarms
            .AnyAsync(f => f.UserId == userId && f.FarmId == farmId);

        if (exists)
            return Ok(new { success = true, following = true });

        _db.UserFollowedFarms.Add(new UserFollowedFarm
        {
            UserId = userId,
            FarmId = farmId,
            FollowedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        return Ok(new { success = true, following = true });
    }

    [Authorize]
    [HttpDelete("{farmId}/follow")]
    public async Task<IActionResult> Unfollow(int farmId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var record = await _db.UserFollowedFarms
            .FirstOrDefaultAsync(f => f.UserId == userId && f.FarmId == farmId);

        if (record != null)
        {
            _db.UserFollowedFarms.Remove(record);
            await _db.SaveChangesAsync();
        }

        return Ok(new { success = true, following = false });
    }

    [Authorize]
    [HttpGet("{farmId}/following")]
    public async Task<IActionResult> IsFollowing(int farmId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var following = await _db.UserFollowedFarms
            .AnyAsync(f => f.UserId == userId && f.FarmId == farmId);

        return Ok(new { following });
    }
}
