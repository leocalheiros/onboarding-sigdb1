using FluentValidation;

namespace OnboardingSIGDB1.Application.UseCases.Cargo.UpdateCargoById;

public class UpdateCargoByIdValidator : AbstractValidator<UpdateCargoByIdCommand>
{
    public UpdateCargoByIdValidator()
    {
        RuleFor(command => command.Descricao)
            .NotEmpty().WithMessage("A descrição do Cargo é obrigatória.")
            .MaximumLength(250).WithMessage("A descrição do cargo não pode ter mais de 250 caracteres");
    }
}