using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HomeGrown.Services;

namespace HomeGrown.Controllers;

[Authorize]
public class FarmerDashboardController : Controller
{
    private readonly MockDataService _data;

    public FarmerDashboardController(MockDataService data)
    {
        _data = data;
    }

    public IActionResult Index()
    {
        var userName = User.Identity?.Name ?? "Farmer";
        var farmIdStr = User.FindFirst("FarmId")?.Value;
        int farmId = int.TryParse(farmIdStr, out var fid) ? fid : 1;

        var farm = _data.GetFarm(farmId) ?? _data.GetFarm(1)!;
        var products = _data.GetProductsByFarm(farm.Id);

        ViewBag.UserName = userName;
        ViewBag.Farm = farm;
        ViewBag.Products = products;

        ViewBag.RecentOrders = new[]
        {
            new { Id = 2041, Customer = "Alex Johnson", Date = "Apr 2, 2026", Status = "Processing", Total = "$34.50" },
            new { Id = 2038, Customer = "Sarah Chen", Date = "Apr 1, 2026", Status = "Shipped", Total = "$18.00" },
            new { Id = 2034, Customer = "Mike Davis", Date = "Mar 30, 2026", Status = "Delivered", Total = "$55.00" },
            new { Id = 2029, Customer = "Rachel S.", Date = "Mar 28, 2026", Status = "Delivered", Total = "$22.50" }
        };

        // Monthly revenue mock data for chart
        ViewBag.ChartLabels = new[] { "Oct", "Nov", "Dec", "Jan", "Feb", "Mar" };
        ViewBag.ChartData = new[] { 840, 1120, 1580, 970, 1340, 1760 };

        return View();
    }
}
