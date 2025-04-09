using ResellFlow.Application.UseCases.Order;
using ResellFlow.Application.UseCases.Reseller;
using ResellFlow.Domain.Interfaces;
using ResellFlow.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using ResellFlow.Infrastructure.Clients;

var builder = WebApplication.CreateBuilder(args);

// UseCases
builder.Services.AddScoped<EmitResellerOrderUseCase>();
builder.Services.AddScoped<CreateOrderUseCase>();
builder.Services.AddScoped<CreateResellerUseCase>();

// Add services to the container
builder.Services.AddControllers();

// Validations (FluentValidation)
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateResellerRequestValidator>();

// Dependency Injection
builder.Services.AddSingleton<IResellerRepository, InMemoryResellerRepository>();
builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddHttpClient<IExternalOrderClient, ExternalOrderHttpClient>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Routing
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();