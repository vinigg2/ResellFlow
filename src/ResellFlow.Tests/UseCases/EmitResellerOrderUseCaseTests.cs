using Moq;
using ResellFlow.Application.UseCases.Order;
using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;

namespace ResellFlow.Tests.UseCases;

public class EmitResellerOrderUseCaseTests
{
    [Fact]
    public async Task Should_Throw_If_No_Orders_Found()
    {
        var orderRepo = new Mock<IOrderRepository>();
        var client = new Mock<IExternalOrderClient>();

        orderRepo.Setup(r => r.GetByClientIdentifierAsync("revenda-x"))
            .ReturnsAsync(new List<Order>());

        var useCase = new EmitResellerOrderUseCase(orderRepo.Object, client.Object);

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync("revenda-x"));
        Assert.Equal("Nenhum pedido encontrado para esta revenda.", ex.Message);
    }

    [Fact]
    public async Task Should_Throw_If_TotalQuantity_Is_Less_Than_1000()
    {
        var orderRepo = new Mock<IOrderRepository>();
        var client = new Mock<IExternalOrderClient>();

        var orders = new List<Order>
        {
            new Order
            {
                ClientIdentifier = "revenda-x",
                Items = new List<OrderItem>
                {
                    new OrderItem { Product = "Produto A", Quantity = 400 },
                    new OrderItem { Product = "Produto B", Quantity = 300 }
                }
            }
        };

        orderRepo.Setup(r => r.GetByClientIdentifierAsync("revenda-x"))
            .ReturnsAsync(orders);

        var useCase = new EmitResellerOrderUseCase(orderRepo.Object, client.Object);

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync("revenda-x"));
        Assert.Equal("A quantidade total dos pedidos da revenda deve ser no mínimo 1000 unidades.", ex.Message);
    }

    [Fact]
    public async Task Should_Emit_Order_Successfully_When_Total_Is_Valid()
    {
        var orderRepo = new Mock<IOrderRepository>();
        var client = new Mock<IExternalOrderClient>();

        var orders = new List<Order>
        {
            new Order
            {
                ClientIdentifier = "revenda-x",
                Items = new List<OrderItem>
                {
                    new OrderItem { Product = "Produto A", Quantity = 400 },
                    new OrderItem { Product = "Produto B", Quantity = 600 }
                }
            }
        };

        new ExternalOrderResponse
        {
            OrderIdExternal = "ext-123",
            Items = new List<OrderItem>
            {
                new OrderItem { Product = "Produto A", Quantity = 400 },
                new OrderItem { Product = "Produto B", Quantity = 600 }
            }
        };

        orderRepo.Setup(r => r.GetByClientIdentifierAsync("revenda-x"))
            .ReturnsAsync(orders);

        client.Setup(c => c.SendOrderAsync(It.IsAny<Order>()))
            .ReturnsAsync(new ExternalOrderResponse
            {
                OrderIdExternal = "ext-123",
                Items = new List<OrderItem>
                {
                    new OrderItem { Product = "Produto A", Quantity = 400 },
                    new OrderItem { Product = "Produto B", Quantity = 600 }
                }
            });

        var useCase = new EmitResellerOrderUseCase(orderRepo.Object, client.Object);

        var result = await useCase.ExecuteAsync("revenda-x");

        Assert.Equal("ext-123", result.OrderIdExternal);
        Assert.Equal(2, result.Items.Count);
    }
}