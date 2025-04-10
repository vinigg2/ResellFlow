using ResellFlow.Application.DTOs;
using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Application.UseCases.Reseller;

public class UpdateResellerUseCase(IResellerRepository repository)
{
    public async Task<ResellerResponse> ExecuteAsync(Guid id, UpdateResellerRequest request)
    {
        var reseller = await repository.GetByIdAsync(id);
        if (reseller == null)
            throw new KeyNotFoundException($"Reseller with Id {id} not found.");

        reseller.Cnpj = request.Cnpj ?? reseller.Cnpj;
        reseller.CorporateName = request.CorporateName ?? reseller.CorporateName;
        reseller.TradeName = request.TradeName ?? reseller.TradeName;
        reseller.Email = request.Email ?? reseller.Email;
        reseller.Phones = request.Phones ?? reseller.Phones;
        reseller.Contacts = request.Contacts?.Select(c => new Contact
        {
            Name = c.Name,
            Email = c.Email
        }).ToList() ?? reseller.Contacts;

        reseller.DeliveryAddresses = request.DeliveryAddresses?.Select(d => new DeliveryAddress
        {
            Street = d.Street,
            District = d.District,
            City = d.City,
            State = d.State,
            ZipCode = d.ZipCode,
            Number = d.Number,
            Complement = d.Complement
        }).ToList() ?? reseller.DeliveryAddresses;

        var updated = await repository.UpdateAsync(reseller);

        return new ResellerResponse
        {
            Id = updated.Id,
            Cnpj = updated.Cnpj,
            CorporateName = updated.CorporateName,
            TradeName = updated.TradeName,
            Email = updated.Email
        };
    }
}