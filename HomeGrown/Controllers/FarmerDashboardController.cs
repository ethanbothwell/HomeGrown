using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeGrown.Data;
using HomeGrown.Services;

namespace HomeGrown.Controllers;

[Authorize]
public class FarmerDashboardController : Controller
{
    private readonly MockDataService _data;
    private readonly HomeGrownDbContext _db;

    public FarmerDashboardController(MockDataService data, HomeGrownDbContext db)
    {
        _data = data;
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var userName = User.Identity?.Name ?? "Farmer";
        var farmIdStr = User.FindFirst("FarmId")?.Value;
        int farmId = int.TryParse(farmIdStr, out var fid) ? fid : 1;

        var farm = _data.GetFarm(farmId) ?? _data.GetFarm(1)!;
        var products = _data.GetProductsByFarm(farm.Id);

        // Real order data for this farm — query OrderItems with matching FarmId
        var now = DateTime.UtcNow;
        var thisMonthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var lastMonthStart = thisMonthStart.AddMonths(-1);

        // Get all orders that contain items from this farm
        var orderIds = await _db.OrderItems
            .Where(oi => oi.FarmId == farmId)
            .Select(oi => oi.OrderId)
            .Distinct()
            .ToListAsync();

        var orders = await _db.Orders
            .Where(o => orderIds.Contains(o.Id))
            .Include(o => o.Items)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        // Calculate farm-specific revenue from order items (not full order totals)
        decimal FarmRevenue(IEnumerable<Models.Order> ordersSet) =>
            ordersSet.SelectMany(o => o.Items)
                .Where(i => i.FarmId == farmId)
                .Sum(i => i.Price * i.Quantity);

        var thisMonthOrders = orders.Where(o => o.OrderDate >= thisMonthStart).ToList();
        var lastMonthOrders = orders.Where(o => o.OrderDate >= lastMonthStart && o.OrderDate < thisMonthStart).ToList();

        var revenueThisMonth = FarmRevenue(thisMonthOrders);
        var revenueLastMonth = FarmRevenue(lastMonthOrders);
        var ordersThisMonth = thisMonthOrders.Count;
        var ordersLastMonth = lastMonthOrders.Count;

        // Trends
        var revenueTrend = revenueLastMonth > 0
            ? (int)Math.Round((revenueThisMonth - revenueLastMonth) / revenueLastMonth * 100)
            : (revenueThisMonth > 0 ? 100 : 0);
        var ordersTrend = ordersThisMonth - ordersLastMonth;

        // Chart data — last 6 months
        var chartLabels = new string[6];
        var chartData = new decimal[6];
        for (int i = 5; i >= 0; i--)
        {
            var monthStart = thisMonthStart.AddMonths(-i);
            var monthEnd = monthStart.AddMonths(1);
            chartLabels[5 - i] = monthStart.ToString("MMM");
            chartData[5 - i] = orders
                .Where(o => o.OrderDate >= monthStart && o.OrderDate < monthEnd)
                .SelectMany(o => o.Items)
                .Where(item => item.FarmId == farmId)
                .Sum(item => item.Price * item.Quantity);
        }

        // Recent orders for this farm
        var recentOrders = orders.Take(5).Select(o =>
        {
            var buyer = _db.Users.Find(o.UserId);
            return new
            {
                o.Id,
                Customer = buyer?.Name ?? "Customer",
                Date = o.OrderDate.ToString("MMM d, yyyy"),
                o.Status,
                Total = "$" + o.Items.Where(i => i.FarmId == farmId).Sum(i => i.Price * i.Quantity).ToString("F2")
            };
        }).ToList();

        ViewBag.UserName = userName;
        ViewBag.Farm = farm;
        ViewBag.Products = products;
        ViewBag.RevenueThisMonth = revenueThisMonth;
        ViewBag.RevenueTrend = revenueTrend;
        ViewBag.OrdersThisMonth = ordersThisMonth;
        ViewBag.OrdersTrend = ordersTrend;
        ViewBag.RecentOrders = recentOrders;
        ViewBag.ChartLabels = chartLabels;
        ViewBag.ChartData = chartData.Select(d => (int)Math.Round(d)).ToArray();

        return View();
    }
}
