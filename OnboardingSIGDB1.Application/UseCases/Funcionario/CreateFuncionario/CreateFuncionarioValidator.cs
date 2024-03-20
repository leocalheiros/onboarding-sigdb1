using FluentValidation;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.CreateFuncionario;

public class CreateFuncionarioValidator : AbstractValidator<CreateFuncionarioCommand>
{
    public CreateFuncionarioValidator()
    {
        RuleFor(command => command.Nome)
            .NotEmpty().WithMessage("O nome do Funcionário é obrigatório.")
            .MaximumLength(150).WithMessage("O nome do funcionário não pode ter mais de 150 caracteres,");

        RuleFor(command => command.Cpf)
            .NotEmpty().WithMessage("O CPF do funcionário é obrigatório.")
            .Length(11).WithMessage("O CPF do funcionário deve ter 11 caracteres.");
        
        RuleFor(command => command.DataContratacao)
            .NotEmpty().WithMessage("A data de contratação do funcionário é obrigatória.")
            .GreaterThanOrEqualTo(DateTimeOffset.MinValue)
            .WithMessage("A data de contratação do funcionário deve ser maior ou igual a " + DateTimeOffset.MinValue + ".");
    }
}