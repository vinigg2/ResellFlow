using ResellFlow.Domain.Entities;

namespace ResellFlow.Domain.Interfaces;

public interface IExternalOrderClient
{
    Task<ExternalOrderResponse> SendOrderAsync(Order order);
}
