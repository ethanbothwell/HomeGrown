using Microsoft.AspNetCore.Mvc;
using HomeGrown.Services;

namespace HomeGrown.Controllers;

public class DiscoverController : Controller
{
    private readonly MockDataService _data;

    public DiscoverController(MockDataService data)
    {
        _data = data;
    }

    public IActionResult Index(string? category, decimal? minPrice, decimal? maxPrice, bool inStockOnly = false, string? sort = null)
    {
        var products = _data.FilterProducts(category, minPrice, maxPrice, inStockOnly, sort);

        ViewBag.Category = category;
        ViewBag.MinPrice = minPrice;
        ViewBag.MaxPrice = maxPrice;
        ViewBag.InStockOnly = inStockOnly;
        ViewBag.Sort = sort;
        ViewBag.ResultCount = products.Count;
        ViewBag.Categories = new[] { "Dairy", "Bread & Baked", "Produce", "Honey & Preserves", "Meat", "Other" };

        return View(products);
    }
}
