using ResellFlow.Application.DTOs;
using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Application.UseCases.Order;

public class CreateOrderUseCase(IOrderRepository repository)
{
    public async Task<OrderResponse> ExecuteAsync(CreateOrderRequest request)
    {
        var order = new Domain.Entities.Order
        {
            Id = Guid.NewGuid(),
            ClientIdentifier = request.ClientIdentifier,
            Items = request.Items.Select(i => new OrderItem
            {
                Product = i.Product,
                Quantity = i.Quantity
            }).ToList()
        };

        var saved = await repository.AddAsync(order);

        return new OrderResponse
        {
            OrderId = saved.Id,
            Items = order.Items
        };
    }
}