using ResellFlow.Domain.Entities;

namespace ResellFlow.Application.DTOs;

public class OrderResponse
{
    public Guid OrderId { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}