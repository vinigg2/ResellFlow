namespace ResellFlow.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public string ClientIdentifier { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}