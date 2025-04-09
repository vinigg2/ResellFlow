namespace ResellFlow.Domain.Entities;

public class ExternalOrderResponse
{
    public string OrderIdExternal { get; set; } = string.Empty;
    public List<OrderItem> Items { get; set; } = new();
}