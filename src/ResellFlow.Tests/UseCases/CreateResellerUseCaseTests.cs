using ResellFlow.Application.DTOs;
using ResellFlow.Application.UseCases.Reseller;
using ResellFlow.Infrastructure.Repositories;

namespace ResellFlow.Tests.UseCases;

public class CreateResellerUseCaseTests
{
    [Fact]
    public async Task Should_Create_Reseller_With_Valid_Data()
    {
        var repository = new InMemoryResellerRepository();
        var useCase = new CreateResellerUseCase(repository);

        var request = new CreateResellerRequest
        {
            Cnpj = "27865757000102",
            CorporateName = "Teste LTDA",
            TradeName = "Teste",
            Email = "contato@teste.com",
            Phones = new List<string> { "11999999999" },
            Contacts = new List<ContactRequest>
            {
                new ContactRequest
                {
                    Name = "Fulano", Email = "fulano@domain.com"
                }
            },
            DeliveryAddresses = new List<DeliveryAddressRequest>
            {
                new DeliveryAddressRequest
                {
                    Street = "Rua das Cervejas", Number = "123", City = "São Paulo", State = "SP",
                    ZipCode = "00000-000", District = "Centro"
                }
            }
        };

        var result = await useCase.ExecuteAsync(request);

        Assert.Equal(request.Cnpj, result.Cnpj);
        Assert.Equal(request.CorporateName, result.CorporateName);
    }

    [Fact]
    public async Task Should_Handle_Empty_Optional_Fields()
    {
        var repository = new InMemoryResellerRepository();
        var useCase = new CreateResellerUseCase(repository);

        var request = new CreateResellerRequest
        {
            Cnpj = "27865757000102",
            CorporateName = "Ambev LTDA",
            TradeName = "Ambev",
            Email = "contato@ambev.com",
            Phones = new List<string>(),
            Contacts = new List<ContactRequest> { new ContactRequest { Name = "Fulano", Email = "fulano@domain.com" } },
            DeliveryAddresses = new List<DeliveryAddressRequest>
            {
                new DeliveryAddressRequest
                {
                    Street = "Rua 1", District = "Centro", City = "São Paulo", State = "SP", ZipCode = "00000-000",
                    Number = "123"
                }
            }
        };

        var result = await useCase.ExecuteAsync(request);

        Assert.NotNull(result);
    }
}