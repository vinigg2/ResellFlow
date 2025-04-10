using ResellFlow.Domain.Entities;

namespace ResellFlow.Application.DTOs;

public class ResellerResponse
{
    public Guid Id { get; set; }
    public string Cnpj { get; set; } = string.Empty;
    public string CorporateName { get; set; } = string.Empty;
    public string TradeName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<string> Phones { get; set; } = new();
    public List<Contact> Contacts { get; set; } = new();
    public List<DeliveryAddress> DeliveryAddresses { get; set; } = new();
}