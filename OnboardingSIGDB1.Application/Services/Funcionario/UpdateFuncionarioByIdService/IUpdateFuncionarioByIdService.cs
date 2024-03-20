using OnboardingSIGDB1.Application.UseCases.Funcionario.UpdateFuncionarioById;

namespace OnboardingSIGDB1.Application.Services.Funcionario.UpdateFuncionarioByIdService;

public interface IUpdateFuncionarioByIdService
{
    Task<Domain.Entities.Funcionario> ChecaValidacoesUpdate(long id, UpdateFuncionarioByIdCommand command, CancellationToken cancellationToken);
}