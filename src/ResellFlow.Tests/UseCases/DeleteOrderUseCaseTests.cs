using Moq;
using ResellFlow.Application.UseCases.Order;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Tests.UseCases;

public class DeleteOrderUseCaseTests
{
    [Fact]
    public async Task Should_Call_DeleteAsync_With_Correct_Id()
    {
        var mockRepo = new Mock<IOrderRepository>();
        var useCase = new DeleteOrderUseCase(mockRepo.Object);

        var id = Guid.NewGuid();
        await useCase.ExecuteAsync(id);

        mockRepo.Verify(x => x.DeleteAsync(id), Times.Once);
    }
}