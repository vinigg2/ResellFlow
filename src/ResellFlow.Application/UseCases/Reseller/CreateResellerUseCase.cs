using ResellFlow.Application.DTOs;
using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Application.UseCases.Reseller;

public class CreateResellerUseCase(IResellerRepository repository)
{
    public async Task<ResellerResponse> ExecuteAsync(CreateResellerRequest request)
    {
        var reseller = new Domain.Entities.Reseller
        {
            Id = Guid.NewGuid(),
            Cnpj = request.Cnpj,
            CorporateName = request.CorporateName,
            TradeName = request.TradeName,
            Email = request.Email,
            Phones = request.Phones ?? new List<string>(),
            Contacts = request.Contacts.Select(c => new Contact
            {
                Name = c.Name,
                Email = c.Email
            }).ToList(),
            DeliveryAddresses = request.DeliveryAddresses.Select(d => new DeliveryAddress
            {
                Street = d.Street,
                District = d.District,
                City = d.City,
                State = d.State,
                ZipCode = d.ZipCode,
                Number = d.Number,
                Complement = d.Complement
            }).ToList()
        };

        var created = await repository.AddAsync(reseller);

        return new ResellerResponse
        {
            Id = created.Id,
            Cnpj = created.Cnpj,
            CorporateName = created.CorporateName,
            TradeName = created.TradeName,
            Email = created.Email
        };
    }
}