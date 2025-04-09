using ResellFlow.Domain.Entities;

namespace ResellFlow.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order> AddAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<List<Order>> GetByClientIdentifierAsync(String clientIdentifier);
}