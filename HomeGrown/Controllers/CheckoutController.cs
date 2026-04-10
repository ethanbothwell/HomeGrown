using Microsoft.AspNetCore.Mvc;
using HomeGrown.Models;
using System.Text.Json;

namespace HomeGrown.Controllers;

public class CheckoutController : Controller
{
    private const string CartKey = "cart";

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
    public IActionResult Confirm(CheckoutViewModel model)
    {
        var cart = GetCart();
        model.Items = cart;
        model.Total = cart.Sum(i => i.Price * i.Quantity);

        // Clear the cart
        HttpContext.Session.Remove(CartKey);

        return View(model);
    }
}
