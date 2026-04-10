using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HomeGrown.Services;

namespace HomeGrown.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly MockDataService _data;

    public DashboardController(MockDataService data)
    {
        _data = data;
    }

    public IActionResult Index()
    {
        ViewBag.UserName = User.Identity?.Name ?? "Guest";
        ViewBag.RecommendedProducts = _data.GetFeaturedProducts(4);
        ViewBag.FollowedFarms = _data.GetFarms().Take(3).ToList();

        // Mock order history
        ViewBag.Orders = new[]
        {
            new { Id = 1001, Date = "March 28, 2026", Status = "Delivered", Total = "$34.50", Items = 3 },
            new { Id = 1002, Date = "March 14, 2026", Status = "Delivered", Total = "$21.00", Items = 2 },
            new { Id = 1003, Date = "February 28, 2026", Status = "Delivered", Total = "$47.00", Items = 4 }
        };

        return View();
    }
}
