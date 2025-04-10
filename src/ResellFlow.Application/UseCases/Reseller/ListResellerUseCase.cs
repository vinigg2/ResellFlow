using ResellFlow.Application.DTOs;
using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Application.UseCases.Reseller;

public class ListResellerUseCase(IResellerRepository repository)
{
    public async Task<List<ResellerResponse>?> ExecuteAsync(Guid? id = null)
    {
        if (id.HasValue)
        {
            var reseller = await repository.GetByIdAsync(id.Value);
            
            if (reseller == null)
                return null;

            return new List<ResellerResponse>
            {
                new ResellerResponse
                {
                    Id = reseller.Id,
                    Cnpj = reseller.Cnpj,
                    CorporateName = reseller.CorporateName,
                    TradeName = reseller.TradeName,
                    Email = reseller.Email
                }
            };
        }

        var resellers = await repository.GetAllAsync();
        return resellers.Select(r => new ResellerResponse
        {
            Id = r.Id,
            Cnpj = r.Cnpj,
            CorporateName = r.CorporateName,
            TradeName = r.TradeName,
            Email = r.Email
        }).ToList();
    }
}