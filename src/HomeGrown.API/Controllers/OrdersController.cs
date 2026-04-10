using System.Security.Claims;
using HomeGrown.Core.Application.DTOs.Orders;
using HomeGrown.Core.Domain.Entities;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeGrown.API.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrdersController(IUnitOfWork uow) : ControllerBase
{
    /// <summary>GET /api/orders — buyer's own orders</summary>
    [HttpGet]
    public async Task<IActionResult> GetMyOrders()
    {
        var orders = await uow.Orders.GetByBuyerIdAsync(GetUserId());
        return Ok(orders.Select(MapToDto));
    }

    /// <summary>GET /api/orders/{id}</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await uow.Orders.GetByIdWithItemsAsync(id);
        if (order is null) return NotFound();
        if (order.BuyerId != GetUserId() && !User.IsInRole("Admin")) return Forbid();
        return Ok(MapToDto(order));
    }

    /// <summary>POST /api/orders — place a new order</summary>
    [HttpPost]
    [Authorize(Roles = "Buyer")]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var items = new List<OrderItem>();
        decimal total = 0;

        foreach (var lineItem in request.Items)
        {
            var product = await uow.Products.GetByIdAsync(lineItem.ProductId);
            if (product is null)
                return BadRequest(new { error = $"Product {lineItem.ProductId} not found." });
            if (!product.InStock)
                return BadRequest(new { error = $"'{product.Name}' is out of stock." });

            var item = new OrderItem
            {
                ProductId = product.Id,
                Quantity = lineItem.Quantity,
                UnitPrice = product.Price   // snapshot price at time of order
            };

            items.Add(item);
            total += product.Price * lineItem.Quantity;
        }

        var order = new Order
        {
            BuyerId = GetUserId(),
            Items = items,
            Total = total,
            ShippingAddress = request.ShippingAddress,
            Notes = request.Notes
        };

        await uow.Orders.AddAsync(order);
        await uow.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = order.Id }, MapToDto(order));
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException());

    private static OrderDto MapToDto(Order o) => new(
        o.Id,
        o.Status.ToString(),
        o.Total,
        o.ShippingAddress,
        o.CreatedAt,
        o.Items.Select(oi => new OrderItemDto(
            oi.ProductId,
            oi.Product?.Name ?? string.Empty,
            oi.Product?.ImageUrl,
            oi.Quantity,
            oi.UnitPrice
        )).ToList()
    );
}
