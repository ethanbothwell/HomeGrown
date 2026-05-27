using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeGrown.Data;
using HomeGrown.Services;

namespace HomeGrown.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly MockDataService _data;
    private readonly HomeGrownDbContext _db;

    public DashboardController(MockDataService data, HomeGrownDbContext db)
    {
        _data = data;
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        // Real stats from database
        var orders = await _db.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.Items)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        var followedFarmIds = await _db.UserFollowedFarms
            .Where(f => f.UserId == userId)
            .Select(f => f.FarmId)
            .ToListAsync();

        ViewBag.UserName = User.Identity?.Name ?? "Guest";
        ViewBag.OrdersPlaced = orders.Count;
        ViewBag.SpentLocally = orders.Sum(o => o.Total);
        ViewBag.FarmsFollowedCount = followedFarmIds.Count;
        ViewBag.ItemsOrdered = orders.SelectMany(o => o.Items).Sum(i => i.Quantity);

        // Recent orders (most recent 5)
        ViewBag.Orders = orders.Take(5).Select(o => new
        {
            o.Id,
            Date = o.OrderDate.ToString("MMMM d, yyyy"),
            o.Status,
            Total = "$" + o.Total.ToString("F2"),
            Items = o.Items.Sum(i => i.Quantity)
        }).ToList();

        // Followed farms — look up from MockDataService by FarmId
        ViewBag.FollowedFarms = followedFarmIds
            .Select(fid => _data.GetFarm(fid))
            .Where(f => f != null)
            .ToList();

        // Recommended products (catalog data, not user-specific)
        ViewBag.RecommendedProducts = _data.GetFeaturedProducts(4);

        return View();
    }
}
