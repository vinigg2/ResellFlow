namespace ResellFlow.Application.DTOs;

public class CreateResellerRequest
{
    public string Cnpj { get; set; } = string.Empty;
    public string CorporateName { get; set; } = string.Empty;
    public string TradeName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<string> Phones { get; set; } = new();
    public List<ContactRequest> Contacts { get; set; } = new();
    public List<DeliveryAddressRequest> DeliveryAddresses { get; set; } = new();
}