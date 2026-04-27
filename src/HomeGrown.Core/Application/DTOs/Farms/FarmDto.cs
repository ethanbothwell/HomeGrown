using HomeGrown.Core.Application.DTOs.Subscriptions;

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
    List<FarmPracticeDto> Practices,
    string OwnerName,
    string? OwnerImageUrl,
    List<SubscriptionPlanDto> SubscriptionPlans
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
