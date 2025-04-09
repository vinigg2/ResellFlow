namespace ResellFlow.Application.DTOs;

public class CreateOrderRequest
{
    public string ClientIdentifier { get; set; }
    public List<CreateOrderItem> Items { get; set; } = new();
}

public class CreateOrderItem
{
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
}