using System.ComponentModel.DataAnnotations;
using HomeGrown.Core.Domain.Enums;

namespace HomeGrown.Core.Application.DTOs.Subscriptions;

public record SubscriptionPlanDto(
    Guid Id,
    Guid FarmId,
    string FarmName,
    string Name,
    string? Description,
    decimal Price,
    SubscriptionFrequency Frequency,
    int? MaxSubscribers,
    bool IsActive,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record CreateSubscriptionPlanRequest(
    [Required][MaxLength(200)] string Name,
    string? Description,
    [Range(0.01, 100000)] decimal Price,
    [Required] SubscriptionFrequency Frequency,
    int? MaxSubscribers
);

public record UpdateSubscriptionPlanRequest(
    [MaxLength(200)] string? Name,
    string? Description,
    [Range(0.01, 100000)] decimal? Price,
    SubscriptionFrequency? Frequency,
    int? MaxSubscribers,
    bool? IsActive
);
