using ResellFlow.Domain.Entities;

namespace ResellFlow.Domain.Interfaces;

public interface IResellerRepository
{
    Task<Reseller> AddAsync(Reseller reseller);
    Task<Reseller?> GetByIdAsync(Guid id);
    Task<IEnumerable<Reseller>> GetAllAsync();
    Task<Reseller> UpdateAsync(Reseller reseller);
    Task DeleteAsync(Guid id);
}