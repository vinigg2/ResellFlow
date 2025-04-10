using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Application.UseCases.Order;

public class DeleteOrderUseCase(IOrderRepository repository)
{
    public Task<object> ExecuteAsync(Guid id)
    {
        return Task.FromResult<object>(repository.DeleteAsync(id));
    }
}