using Microsoft.AspNetCore.Mvc;
using HomeGrown.Models;
using HomeGrown.Services;
using System.Text.Json;

namespace HomeGrown.Controllers;

public class CartController : Controller
{
    private readonly MockDataService _data;
    private const string CartKey = "cart";

    public CartController(MockDataService data)
    {
        _data = data;
    }

    private List<CartItem> GetCart()
    {
        var json = HttpContext.Session.GetString(CartKey);
        return string.IsNullOrEmpty(json)
            ? new List<CartItem>()
            : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
    }

    private void SaveCart(List<CartItem> cart)
    {
        HttpContext.Session.SetString(CartKey, JsonSerializer.Serialize(cart));
    }

    public IActionResult Index()
    {
        var cart = GetCart();
        ViewBag.Total = cart.Sum(i => i.Price * i.Quantity);
        return View(cart);
    }

    [HttpPost]
    public IActionResult Add(int productId, int quantity = 1)
    {
        var product = _data.GetProduct(productId);
        if (product == null) return NotFound();

        var cart = GetCart();
        var existing = cart.FirstOrDefault(i => i.ProductId == productId);
        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                FarmName = product.FarmName,
                ImageUrl = product.ImageUrl,
                Quantity = quantity,
                Price = product.Price
            });
        }
        SaveCart(cart);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Remove(int productId)
    {
        var cart = GetCart();
        cart.RemoveAll(i => i.ProductId == productId);
        SaveCart(cart);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int productId, int quantity)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            if (quantity <= 0)
                cart.Remove(item);
            else
                item.Quantity = quantity;
        }
        SaveCart(cart);
        return RedirectToAction("Index");
    }
}
