using Moq;
using ResellFlow.Application.UseCases.Reseller;
using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Tests.UseCases;

public class DeleteResellerUseCaseTests
{
    [Fact]
    public async Task Should_Delete_Reseller()
    {
        var id = Guid.NewGuid();
        var mockRepo = new Mock<IResellerRepository>();
        mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Reseller { Id = id });

        var useCase = new DeleteResellerUseCase(mockRepo.Object);
        await useCase.ExecuteAsync(id);

        mockRepo.Verify(x => x.DeleteAsync(id), Times.Once);
    }
}