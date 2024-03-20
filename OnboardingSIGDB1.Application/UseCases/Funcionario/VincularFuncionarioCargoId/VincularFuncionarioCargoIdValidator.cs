using FluentValidation;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioCargoId;

public class VincularFuncionarioCargoIdValidator : AbstractValidator<VincularFuncionarioCargoIdCommand>
{
    public VincularFuncionarioCargoIdValidator()
    {
        RuleFor(command => command.FuncionarioId)
            .NotEmpty().WithMessage("O ID do funcionário é obrigatório!");
        
        RuleFor(command => command.CargoId)
            .NotEmpty().WithMessage("O ID do Cargo é obrigatório!");
        
        RuleFor(command => command.DataVinculo)
            .NotEmpty().WithMessage("A data de vínculo do cargo é obrigatória.")
            .GreaterThanOrEqualTo(DateTimeOffset.MinValue)
            .WithMessage("A data de vínculo do cargo deve ser maior ou igual a " + DateTimeOffset.MinValue + ".");
    }
}