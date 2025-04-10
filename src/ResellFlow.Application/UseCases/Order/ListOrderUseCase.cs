using ResellFlow.Application.DTOs;
using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Application.UseCases.Order;

public class ListOrderUseCase(IOrderRepository repository)
{
    public async Task<object> ExecuteAsync(Guid? id)
    {
        if (id.HasValue)
        {
            var order = await repository.GetByIdAsync(id.Value);
            
            if (order == null)
                return null;

            return new OrderResponse
            {
                OrderId = order.Id,
                Items = order.Items
            };;
        }
        
        var orders = await repository.GetAllAsync();

        return orders.Select(order => new OrderResponse
        {
            OrderId = order.Id,
            Items = order.Items
        }).ToList();

    }
}