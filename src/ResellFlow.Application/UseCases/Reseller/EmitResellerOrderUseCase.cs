using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Application.UseCases.Order;

public class EmitResellerOrderUseCase(
    IOrderRepository orderRepository,
    IExternalOrderClient externalOrderClient)
{
    public async Task<ExternalOrderResponse> ExecuteAsync(string clientIdentifier)
    {
        var orders = await orderRepository.GetByClientIdentifierAsync(clientIdentifier);

        if (orders == null || !orders.Any())
            throw new Exception("Nenhum pedido encontrado para esta revenda.");

        var totalQuantity = orders
            .SelectMany(o => o.Items)
            .Sum(i => i.Quantity);

        if (totalQuantity < 1000)
            throw new Exception("A quantidade total dos pedidos da revenda deve ser no mínimo 1000 unidades.");

        var aggregatedItems = orders
            .SelectMany(o => o.Items)
            .GroupBy(i => i.Product)
            .Select(g => new OrderItem
            {
                Product = g.Key,
                Quantity = g.Sum(i => i.Quantity)
            })
            .ToList();

        var order = new Domain.Entities.Order
        {
            ClientIdentifier = clientIdentifier,
            Items = aggregatedItems
        };

        return await externalOrderClient.SendOrderAsync(order);
    }
}