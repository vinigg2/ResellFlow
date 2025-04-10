using Moq;
using ResellFlow.Application.UseCases.Order;
using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Tests.UseCases;

public class ListOrderUseCaseTests
{
    [Fact]
    public async Task Should_Return_All_Orders()
    {
        var mockRepo = new Mock<IOrderRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Order>
        {
            new Order { Id = Guid.NewGuid(), Items = new List<OrderItem>() }
        });

        var useCase = new ListOrderUseCase(mockRepo.Object);
        var result = await useCase.ExecuteAsync();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_Return_Order_By_Id()
    {
        var id = Guid.NewGuid();
        var mockRepo = new Mock<IOrderRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(new Order { Id = id, Items = new List<OrderItem>() });

        var useCase = new ListOrderUseCase(mockRepo.Object);
        var result = await useCase.ExecuteAsync(id);

        Assert.NotNull(result);
    }
}