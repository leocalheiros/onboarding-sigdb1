using OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioEmpresaId;

namespace OnboardingSIGDB1.Application.Services.Funcionario.VincularFuncionarioEmpresaIdService;

public interface IVincularFuncionarioEmpresaIdService
{
    Task<Domain.Entities.Funcionario> VincularFuncionarioEmpresaAsync(VincularFuncionarioEmpresaIdCommand command,
        CancellationToken cancellationToken);
}