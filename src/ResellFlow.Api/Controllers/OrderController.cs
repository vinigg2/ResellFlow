using ResellFlow.Application.DTOs;
using ResellFlow.Application.UseCases.Order;
using ResellFlow.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ResellFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderRepository repository) : ControllerBase
{
    private readonly CreateOrderUseCase _createUseCase = new(repository);
    private readonly ListOrderUseCase _listUseCase = new(repository);
    private readonly DeleteOrderUseCase _deleteUseCase = new(repository);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var response = await _createUseCase.ExecuteAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.OrderId }, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _listUseCase.ExecuteAsync(id);
        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _listUseCase.ExecuteAsync();
        return Ok(orders);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _deleteUseCase.ExecuteAsync(id);
        return Ok();
    }
}