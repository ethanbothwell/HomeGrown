using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HomeGrown.Models;
using HomeGrown.Services;

namespace HomeGrown.Controllers;

public class HomeController : Controller
{
    private readonly MockDataService _data;

    public HomeController(MockDataService data)
    {
        _data = data;
    }

    public IActionResult Index()
    {
        ViewBag.FeaturedProducts = _data.GetFeaturedProducts(6);
        ViewBag.Farms = _data.GetFarms();
        ViewBag.SpotlightFarm = _data.GetFarm(1);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public new IActionResult NotFound()
    {
        Response.StatusCode = 404;
        return View("NotFound");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
