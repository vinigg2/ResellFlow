namespace ResellFlow.Domain.Entities;

public class OrderItem
{
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
}