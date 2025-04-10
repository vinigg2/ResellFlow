using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Infrastructure.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();

    public Task<Order> AddAsync(Order order)
    {
        _orders.Add(order);
        return Task.FromResult(order);
    }

    public Task<Order?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));
    }

    public Task<IEnumerable<Order>> GetAllAsync()
    {
        return Task.FromResult(_orders.AsEnumerable());
    }

    public Task<List<Order>> GetByClientIdentifierAsync(string clientIdentifier)
    {
        var orders = _orders
            .Where(o => o.ClientIdentifier.Equals(clientIdentifier, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Task.FromResult(orders);
    }
    
    public Task DeleteAsync(Guid Id)
    {
        var orderToDelete = _orders.FirstOrDefault(o => o.Id == Id);
        _orders.Remove(orderToDelete);
        return Task.CompletedTask;
    }
}