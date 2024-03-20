namespace OnboardingSIGDB1.Application.Services.Funcionario.FindFuncionarioByIdService;

public interface IFindFuncionarioByIdService
{
    Task<Domain.Entities.Funcionario> ValidaFuncionarioExistente(long id,
        CancellationToken cancellationToken);
}