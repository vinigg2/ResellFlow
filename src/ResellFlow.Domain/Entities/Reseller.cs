namespace ResellFlow.Domain.Entities;

public class Reseller
{
    public Guid Id { get; set; }
    public string Cnpj { get; set; } = default!;
    public string CorporateName { get; set; } = default!;
    public string TradeName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public List<string> Phones { get; set; } = new();
    public List<Contact> Contacts { get; set; } = new();
    public List<DeliveryAddress> DeliveryAddresses { get; set; } = new();
}