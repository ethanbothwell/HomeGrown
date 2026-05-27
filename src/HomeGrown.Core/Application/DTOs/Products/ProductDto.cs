using System.ComponentModel.DataAnnotations;

namespace HomeGrown.Core.Application.DTOs.Products;

public record ProductDto(
    Guid Id,
    Guid FarmId,
    string FarmName,
    string Name,
    string? Description,
    string Category,
    decimal Price,
    string? Unit,
    string? ImageUrl,
    bool InStock,
    string? Tags
);

public record CreateProductRequest(
    [Required] string Name,
    string? Description,
    [Required] string Category,
    [Range(0.01, 10000)] decimal Price,
    string? Unit,
    string? ImageUrl,
    bool InStock = true,
    string? Tags = null
);

public record UpdateProductRequest(
    string? Name,
    string? Description,
    string? Category,
    decimal? Price,
    string? Unit,
    string? ImageUrl,
    bool? InStock,
    string? Tags
);
