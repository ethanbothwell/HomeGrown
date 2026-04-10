using Microsoft.AspNetCore.Mvc;
using HomeGrown.Services;

namespace HomeGrown.Controllers;

public class ProductsController : Controller
{
    private readonly MockDataService _data;

    public ProductsController(MockDataService data)
    {
        _data = data;
    }

    public IActionResult Index(int id)
    {
        var product = _data.GetProduct(id);
        if (product == null) return NotFound();

        var related = _data.GetProductsByFarm(product.FarmId)
                           .Where(p => p.Id != id)
                           .Take(3)
                           .ToList();

        ViewBag.Product = product;
        ViewBag.Farm = _data.GetFarm(product.FarmId);
        ViewBag.RelatedProducts = related;
        return View();
    }
}
