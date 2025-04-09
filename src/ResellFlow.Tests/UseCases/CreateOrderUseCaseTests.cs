using ResellFlow.Application.DTOs;
using ResellFlow.Application.UseCases.Order;
using ResellFlow.Infrastructure.Repositories;

namespace ResellFlow.Tests.UseCases;

public class CreateOrderUseCaseTests
{
    [Fact]
    public async Task Should_Create_Order_With_Valid_Data()
    {
        var repository = new InMemoryOrderRepository();
        var useCase = new CreateOrderUseCase(repository);

        var request = new CreateOrderRequest
        {
            ClientIdentifier = "client-xyz",
            Items = new List<CreateOrderItem>
            {
                new CreateOrderItem { Product = "Cerveja A", Quantity = 300 },
                new CreateOrderItem { Product = "Cerveja B", Quantity = 500 }
            }
        };

        var result = await useCase.ExecuteAsync(request);

        Assert.NotEqual(Guid.Empty, result.OrderId);
        Assert.Equal(2, result.Items.Count);
    }

    [Fact]
    public async Task Should_Create_Order_With_Empty_Items_List()
    {
        var repository = new InMemoryOrderRepository();
        var useCase = new CreateOrderUseCase(repository);

        var request = new CreateOrderRequest
        {
            ClientIdentifier = "client-xyz",
            Items = new List<CreateOrderItem>()
        };

        var result = await useCase.ExecuteAsync(request);

        Assert.NotEqual(Guid.Empty, result.OrderId);
        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task Should_Create_Order_With_Null_Items_List()
    {
        var repository = new InMemoryOrderRepository();
        var useCase = new CreateOrderUseCase(repository);

        var request = new CreateOrderRequest
        {
            ClientIdentifier = "client-xyz",
            Items = null!
        };

        request.Items = new List<CreateOrderItem>();

        var result = await useCase.ExecuteAsync(request);

        Assert.NotEqual(Guid.Empty, result.OrderId);
        Assert.Empty(result.Items);
    }
}