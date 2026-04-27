using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using HomeGrown.Data;
using HomeGrown.Models;
using HomeGrown.Services;
using System.Text.Json;

namespace HomeGrown.Controllers;

public class CheckoutController : Controller
{
    private const string CartKey = "cart";
    private readonly HomeGrownDbContext _db;
    private readonly MockDataService _data;

    public CheckoutController(HomeGrownDbContext db, MockDataService data)
    {
        _db = db;
        _data = data;
    }

    private List<CartItem> GetCart()
    {
        var json = HttpContext.Session.GetString(CartKey);
        return string.IsNullOrEmpty(json)
            ? new List<CartItem>()
            : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
    }

    public IActionResult Index()
    {
        var cart = GetCart();
        if (!cart.Any()) return RedirectToAction("Index", "Cart");

        var model = new CheckoutViewModel
        {
            Items = cart,
            Total = cart.Sum(i => i.Price * i.Quantity)
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Confirm(CheckoutViewModel model)
    {
        var cart = GetCart();
        if (!cart.Any()) return RedirectToAction("Index", "Cart");

        model.Items = cart;
        model.Total = cart.Sum(i => i.Price * i.Quantity);

        // Save the order if user is logged in
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(userIdStr, out var userId))
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = "Processing",
                Total = model.Total,
                Items = cart.Select(ci =>
                {
                    var product = _data.GetProduct(ci.ProductId);
                    return new OrderItem
                    {
                        ProductId = ci.ProductId,
                        ProductName = ci.ProductName,
                        FarmName = ci.FarmName,
                        FarmId = product?.FarmId ?? 0,
                        ImageUrl = ci.ImageUrl,
                        Quantity = ci.Quantity,
                        Price = ci.Price
                    };
                }).ToList()
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
        }

        // Clear the cart
        HttpContext.Session.Remove(CartKey);

        return View(model);
    }
}
