using Moq;
using ResellFlow.Application.UseCases.Reseller;
using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Tests.UseCases;

public class ListResellerUseCaseTests
{
    [Fact]
    public async Task Should_List_All_Resellers()
    {
        var mockRepo = new Mock<IResellerRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Reseller> { new Reseller() });

        var useCase = new ListResellerUseCase(mockRepo.Object);
        var result = await useCase.ExecuteAsync();

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task Should_List_Reseller_By_Id()
    {
        var id = Guid.NewGuid();
        var mockRepo = new Mock<IResellerRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(new Reseller { Id = id });

        var useCase = new ListResellerUseCase(mockRepo.Object);
        var result = await useCase.ExecuteAsync(id);

        Assert.NotNull(result);
        Assert.Single(result);
    }
}