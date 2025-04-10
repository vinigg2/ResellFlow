using ResellFlow.Application.DTOs;

namespace ResellFlow.Application.DTOs;

public class UpdateResellerRequest
{
    public string? Cnpj { get; set; }
    public string? CorporateName { get; set; }
    public string? TradeName { get; set; }
    public string? Email { get; set; }
    public List<string>? Phones { get; set; }
    public List<ContactRequest>? Contacts { get; set; }
    public List<DeliveryAddressRequest>? DeliveryAddresses { get; set; }
}
