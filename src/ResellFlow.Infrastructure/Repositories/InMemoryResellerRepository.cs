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
    
    public Task<Reseller> UpdateAsync(Reseller reseller)
    {
        var existingReseller = _resellers.FirstOrDefault(r => r.Id == reseller.Id);
        if (existingReseller == null)
        {
            throw new KeyNotFoundException($"Reseller with Id {reseller.Id} not found.");
        }

        existingReseller.Cnpj = reseller.Cnpj ?? existingReseller.Cnpj;
        existingReseller.CorporateName = reseller.CorporateName ?? existingReseller.CorporateName;
        existingReseller.TradeName = reseller.TradeName ?? existingReseller.TradeName;
        existingReseller.Email = reseller.Email ?? existingReseller.Email;
        existingReseller.Phones = reseller.Phones ?? existingReseller.Phones;
        existingReseller.Contacts = reseller.Contacts ?? existingReseller.Contacts;
        existingReseller.DeliveryAddresses = reseller.DeliveryAddresses ?? existingReseller.DeliveryAddresses;

        return Task.FromResult(existingReseller);
    }
    
    public Task DeleteAsync(Guid id)
    {
        var reseller = _resellers.FirstOrDefault(r => r.Id == id);
        if (reseller == null)
        {
            throw new KeyNotFoundException($"Reseller with Id {id} not found.");
        }

        _resellers.Remove(reseller);
        return Task.CompletedTask;
    }
}