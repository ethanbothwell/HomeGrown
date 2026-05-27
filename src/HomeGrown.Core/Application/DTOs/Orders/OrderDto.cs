using System.ComponentModel.DataAnnotations;

namespace HomeGrown.Core.Application.DTOs.Orders;

public record OrderDto(
    Guid Id,
    string Status,
    decimal Total,
    string? ShippingAddress,
    DateTime CreatedAt,
    List<OrderItemDto> Items
);

public record OrderItemDto(
    Guid ProductId,
    string ProductName,
    string? ProductImageUrl,
    int Quantity,
    decimal UnitPrice
);

public record CreateOrderRequest(
    [Required, MinLength(1)] List<CreateOrderItemRequest> Items,
    string? ShippingAddress,
    string? Notes
);

public record CreateOrderItemRequest(
    [Required] Guid ProductId,
    [Range(1, 100)] int Quantity
);
