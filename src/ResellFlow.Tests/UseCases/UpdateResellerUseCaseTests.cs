using Moq;
using ResellFlow.Application.DTOs;
using ResellFlow.Application.UseCases.Reseller;
using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Tests.UseCases;

public class UpdateResellerUseCaseTests
{
    [Fact]
    public async Task Should_Update_Reseller()
    {
        var id = Guid.NewGuid();
        var existing = new Reseller { Id = id, Cnpj = "123", CorporateName = "Old", TradeName = "Old", Email = "old@mail.com" };
        var updated = new Reseller { Id = id, Cnpj = "456", CorporateName = "New", TradeName = "New", Email = "new@mail.com" };

        var mockRepo = new Mock<IResellerRepository>();
        mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(existing);
        mockRepo.Setup(x => x.UpdateAsync(It.IsAny<Reseller>())).ReturnsAsync(updated);

        var request = new UpdateResellerRequest
        {
            Cnpj = "456",
            CorporateName = "New",
            TradeName = "New",
            Email = "new@mail.com"
        };

        var useCase = new UpdateResellerUseCase(mockRepo.Object);
        var result = await useCase.ExecuteAsync(id, request);

        Assert.Equal("456", result.Cnpj);
        Assert.Equal("New", result.CorporateName);
    }
}