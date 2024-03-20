using FluentValidation;

namespace OnboardingSIGDB1.Application.UseCases.Cargo;

public class CreateCargoValidator : AbstractValidator<CreateCargoCommand>
{
    public CreateCargoValidator()
    {
        RuleFor(command => command.Descricao)
            .NotEmpty().WithMessage("A descrição do cargo é obrigatória.")
            .MaximumLength(150).WithMessage("O nome da empresa não pode ter mais de 150 caracteres,");
    }
}