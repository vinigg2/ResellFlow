using ResellFlow.Application.DTOs;
using ResellFlow.Application.UseCases.Reseller;
using ResellFlow.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ResellFlow.Application.UseCases.Order;

namespace ResellFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResellerController : ControllerBase
{
    private readonly CreateResellerUseCase _createUseCase;
    private readonly IResellerRepository _repository;

    public ResellerController(IResellerRepository repository)
    {
        _repository = repository;
        _createUseCase = new CreateResellerUseCase(repository);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResellerRequest request)
    {
        var response = await _createUseCase.ExecuteAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
    
    [HttpPost("{clientIdentifier}/emitir-pedido")]
    public async Task<IActionResult> EmitOrderFromReseller(
        string clientIdentifier,
        [FromServices] EmitResellerOrderUseCase useCase)
    {
        try
        {
            var result = await useCase.ExecuteAsync(clientIdentifier);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var reseller = await _repository.GetByIdAsync(id);
        if (reseller == null)
            return NotFound();

        return Ok(reseller);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resellers = await _repository.GetAllAsync();
        return Ok(resellers);
    }
}