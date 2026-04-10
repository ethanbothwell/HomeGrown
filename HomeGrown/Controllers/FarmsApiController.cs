using HomeGrown.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeGrown.Controllers;

[ApiController]
[Route("api/farms")]
public class FarmsApiController : ControllerBase
{
    [HttpGet]
    public IActionResult GetFarms([FromQuery] string? category = null)
    {
        var farms = SonomaFarmsService.Farms.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(category) && category != "All")
            farms = farms.Where(f => f.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

        var result = farms.Select(f => new
        {
            f.Id,
            f.Name,
            f.Description,
            f.Latitude,
            f.Longitude,
            f.Category,
            f.ImageUrl,
            f.Rating,
            f.ProductCount,
            f.IsOpen,
            f.DistanceMi
        });

        return Ok(result);
    }
}
