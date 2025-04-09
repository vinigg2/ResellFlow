using ResellFlow.Domain.Entities;
using ResellFlow.Domain.Interfaces;
using Polly;
using Polly.Retry;

namespace ResellFlow.Infrastructure.Clients;

public class ExternalOrderHttpClient : IExternalOrderClient
{
    private readonly HttpClient _httpClient;
    private readonly AsyncRetryPolicy<ExternalOrderResponse> _retryPolicy;

    public ExternalOrderHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _retryPolicy = Policy<ExternalOrderResponse>
            .Handle<HttpRequestException>()
            .Or<TaskCanceledException>()
            .OrResult(r => string.IsNullOrWhiteSpace(r.OrderIdExternal))
            .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2));
    }
 
    public async Task<ExternalOrderResponse> SendOrderAsync(Order order)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var rnd = new Random();
            var shouldFail = rnd.Next(1, 5) == 3;

            if (shouldFail)
                throw new HttpRequestException("Erro simulado na API da ResellFlow");

            await Task.Delay(500);

            return new ExternalOrderResponse
            {
                OrderIdExternal = Guid.NewGuid().ToString(),
                Items = order.Items
            };
        });
    }
}