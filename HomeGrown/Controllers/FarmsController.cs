using Microsoft.AspNetCore.Mvc;
using HomeGrown.Services;

namespace HomeGrown.Controllers;

public class FarmsController : Controller
{
    private readonly MockDataService _data;

    public FarmsController(MockDataService data)
    {
        _data = data;
    }

    public IActionResult Index(int id)
    {
        var farm = _data.GetFarm(id);
        if (farm == null) return NotFound();

        ViewBag.Farm = farm;
        ViewBag.Products = _data.GetProductsByFarm(id);
        ViewBag.Reviews = _data.GetReviewsByFarm(id);
        return View();
    }
}
