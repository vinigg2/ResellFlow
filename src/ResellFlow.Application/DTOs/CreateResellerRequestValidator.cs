using FluentValidation;
using ResellFlow.Application.DTOs;
using ResellFlow.Application.Validators;

public class CreateResellerRequestValidator : AbstractValidator<CreateResellerRequest>
{
    public CreateResellerRequestValidator()
    {
        RuleFor(x => x.Cnpj)
            .NotEmpty().WithMessage("CNPJ é obrigatório.")
            .Must(CnpjValidator.IsValid).WithMessage("CNPJ inválido.");

        RuleFor(x => x.CorporateName)
            .NotEmpty().WithMessage("Razão Social é obrigatória.");

        RuleFor(x => x.TradeName)
            .NotEmpty().WithMessage("Nome Fantasia é obrigatório.");

        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.Contacts)
            .NotEmpty().WithMessage("Pelo menos um nome de contato é necessário.");

        RuleFor(x => x.DeliveryAddresses)
            .NotEmpty().WithMessage("Pelo menos um endereço de entrega é necessário.");
    }
}