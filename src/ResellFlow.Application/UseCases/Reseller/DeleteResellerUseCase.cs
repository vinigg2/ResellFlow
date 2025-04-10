using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Application.UseCases.Reseller;

public class DeleteResellerUseCase(IResellerRepository repository)
{
    public async Task ExecuteAsync(Guid id)
    {
        var reseller = await repository.GetByIdAsync(id);
        await repository.DeleteAsync(id);
    }
}