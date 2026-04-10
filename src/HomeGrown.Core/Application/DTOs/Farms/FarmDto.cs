namespace HomeGrown.Core.Application.DTOs.Farms;

public record FarmDto(
    Guid Id,
    string Name,
    string? Bio,
    string? Philosophy,
    string? Location,
    string? City,
    string? State,
    double? Latitude,
    double? Longitude,
    string? ImageUrl,
    double Rating,
    int ReviewCount,
    List<string> Practices,
    string OwnerName,
    string? OwnerImageUrl
);

public record CreateFarmRequest(
    string Name,
    string? Bio,
    string? Philosophy,
    string? Location,
    string? City,
    string? State,
    double? Latitude,
    double? Longitude,
    string? ImageUrl
);

public record UpdateFarmRequest(
    string? Name,
    string? Bio,
    string? Philosophy,
    string? Location,
    string? City,
    string? State,
    double? Latitude,
    double? Longitude,
    string? ImageUrl
);
