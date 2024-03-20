using OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioCargoId;

namespace OnboardingSIGDB1.Application.Services.Funcionario.VincularFuncionarioCargoid;

public interface IVincularFuncionarioCargoIdService
{
    Task<Domain.Entities.FuncionarioCargo> VincularFuncionarioCargoIdAsync(
        VincularFuncionarioCargoIdCommand command,
        CancellationToken cancellationToken);
    
    
}