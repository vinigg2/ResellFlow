namespace ResellFlow.Application.DTOs;

public class OrderResponse
{
    public Guid OrderId { get; set; }
    public List<CreateOrderItem> Items { get; set; } = new();
}