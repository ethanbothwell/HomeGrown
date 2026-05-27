using HomeGrown.Core.Domain.Enums;

namespace HomeGrown.Core.Application.DTOs.Subscriptions;

public record FarmSubscriptionDto(
    Guid Id,
    Guid SubscriptionPlanId,
    string PlanName,
    string FarmName,
    decimal Price,
    SubscriptionFrequency Frequency,
    SubscriptionStatus Status,
    DateTime StartDate,
    DateTime NextDeliveryDate,
    string? ShippingAddress,
    string? Notes,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record SubscribeRequest(string? ShippingAddress, string? Notes);
