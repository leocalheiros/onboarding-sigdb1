using FluentValidation;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioEmpresaId;

public class VincularFuncionarioEmpresaIdValidator : AbstractValidator<VincularFuncionarioEmpresaIdCommand>
{
    public VincularFuncionarioEmpresaIdValidator()
    {
        RuleFor(command => command.FuncionarioId)
            .NotEmpty().WithMessage("O ID do funcionário é obrigatório!");
        
        RuleFor(command => command.EmpresaId)
            .NotEmpty().WithMessage("O ID da Empresa é obrigatório!");
    }
}