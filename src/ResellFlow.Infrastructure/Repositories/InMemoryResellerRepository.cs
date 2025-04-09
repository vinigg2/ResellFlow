using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Infrastructure.Repositories;

public class InMemoryResellerRepository : IResellerRepository
{
    private readonly List<Reseller> _resellers = new();

    public Task<Reseller> AddAsync(Reseller reseller)
    {
        _resellers.Add(reseller);
        return Task.FromResult(reseller);
    }

    public Task<IEnumerable<Reseller>> GetAllAsync() =>
        Task.FromResult(_resellers.AsEnumerable());

    public Task<Reseller?> GetByIdAsync(Guid id) =>
        Task.FromResult(_resellers.FirstOrDefault(r => r.Id == id));
}