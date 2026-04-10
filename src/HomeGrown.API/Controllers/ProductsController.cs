using System.Security.Claims;
using HomeGrown.Core.Application.DTOs.Products;
using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeGrown.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(IUnitOfWork uow) : ControllerBase
{
    /// <summary>GET /api/products — public, supports filtering and sorting</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? category,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] bool? inStockOnly,
        [FromQuery] string? sort)
    {
        var products = await uow.Products.SearchAsync(category, minPrice, maxPrice, inStockOnly, sort);
        return Ok(products.Select(MapToDto));
    }

    /// <summary>GET /api/products/{id} — public</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await uow.Products.GetByIdAsync(id);
        return product is null ? NotFound() : Ok(MapToDto(product));
    }

    /// <summary>POST /api/products — farmer only</summary>
    [HttpPost]
    [Authorize(Roles = "Farmer")]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var farm = await uow.Farms.GetByOwnerIdAsync(GetUserId());
        if (farm is null)
            return BadRequest(new { error = "You must create a farm before adding products." });

        var product = new Product
        {
            FarmId = farm.Id,
            Name = request.Name,
            Description = request.Description,
            Category = request.Category,
            Price = request.Price,
            Unit = request.Unit,
            ImageUrl = request.ImageUrl,
            InStock = request.InStock,
            Tags = request.Tags
        };

        await uow.Products.AddAsync(product);
        await uow.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, MapToDto(product));
    }

    /// <summary>PUT /api/products/{id} — farmer (own product) or admin</summary>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Farmer,Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request)
    {
        var product = await uow.Products.GetByIdAsync(id);
        if (product is null) return NotFound();

        // Verify the farmer owns this product's farm
        if (!User.IsInRole("Admin"))
        {
            var farm = await uow.Farms.GetByOwnerIdAsync(GetUserId());
            if (farm is null || product.FarmId != farm.Id) return Forbid();
        }

        if (request.Name is not null) product.Name = request.Name;
        if (request.Description is not null) product.Description = request.Description;
        if (request.Category is not null) product.Category = request.Category;
        if (request.Price.HasValue) product.Price = request.Price.Value;
        if (request.Unit is not null) product.Unit = request.Unit;
        if (request.ImageUrl is not null) product.ImageUrl = request.ImageUrl;
        if (request.InStock.HasValue) product.InStock = request.InStock.Value;
        if (request.Tags is not null) product.Tags = request.Tags;
        product.UpdatedAt = DateTime.UtcNow;

        uow.Products.Update(product);
        await uow.SaveChangesAsync();

        return Ok(MapToDto(product));
    }

    /// <summary>DELETE /api/products/{id} — farmer (own product) or admin</summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Farmer,Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await uow.Products.GetByIdAsync(id);
        if (product is null) return NotFound();

        if (!User.IsInRole("Admin"))
        {
            var farm = await uow.Farms.GetByOwnerIdAsync(GetUserId());
            if (farm is null || product.FarmId != farm.Id) return Forbid();
        }

        uow.Products.Remove(product);
        await uow.SaveChangesAsync();

        return NoContent();
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException());

    private static ProductDto MapToDto(Product p) => new(
        p.Id, p.FarmId, p.Farm?.Name ?? string.Empty,
        p.Name, p.Description, p.Category, p.Price, p.Unit, p.ImageUrl, p.InStock, p.Tags
    );
}
