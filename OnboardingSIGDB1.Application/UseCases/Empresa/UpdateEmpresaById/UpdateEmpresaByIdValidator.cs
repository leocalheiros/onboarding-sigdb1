using FluentValidation;

namespace OnboardingSIGDB1.Application.UseCases.Empresa.UpdateEmpresaById;

public class UpdateEmpresaByIdValidator : AbstractValidator<UpdateEmpresaByIdCommand>
{
    public UpdateEmpresaByIdValidator()
    {
        RuleFor(command => command.Nome)
            .NotEmpty().WithMessage("O nome da empresa é obrigatório.")
            .MaximumLength(150).WithMessage("O nome da empresa não pode ter mais de 150 caracteres,");

        RuleFor(command => command.Cnpj)
            .NotEmpty().WithMessage("O CNPJ da empresa é obrigatório.")
            .Length(14).WithMessage("O CNPJ da empresa deve ter 14 caracteres.");
        
        RuleFor(command => command.DataFundacao)
            .NotEmpty().WithMessage("A data de fundação da empresa é obrigatória.")
            .GreaterThanOrEqualTo(DateTimeOffset.MinValue)
            .WithMessage("A data de fundação da empresa deve ser maior ou igual a " + DateTimeOffset.MinValue + ".");
    }
}