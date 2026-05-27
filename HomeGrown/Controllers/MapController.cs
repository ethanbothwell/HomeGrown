using Microsoft.AspNetCore.Mvc;
using HomeGrown.Services;
using System.Text.Json;

namespace HomeGrown.Controllers;

public class MapController : Controller
{
    private readonly MockDataService _data;

    public MapController(MockDataService data)
    {
        _data = data;
    }

    public IActionResult Index()
    {
        var farms = _data.GetFarms();
        ViewBag.Farms = farms;
        ViewBag.FarmsJson = JsonSerializer.Serialize(farms.Select(f => new
        {
            f.Id,
            f.Name,
            f.OwnerName,
            f.City,
            f.State,
            f.Location,
            f.Latitude,
            f.Longitude,
            f.Rating,
            f.ReviewCount,
            f.ImageUrl,
            ProductCount = MockDataService.Products.Count(p => p.FarmId == f.Id)
        }));
        return View();
    }
}
