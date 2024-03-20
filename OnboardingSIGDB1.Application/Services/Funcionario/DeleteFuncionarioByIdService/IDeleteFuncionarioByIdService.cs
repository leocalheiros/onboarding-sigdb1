namespace OnboardingSIGDB1.Application.Services.Funcionario.DeleteFuncionarioByIdService;

public interface IDeleteFuncionarioByIdService
{
    Task<Domain.Entities.Funcionario> ChecaFuncionarioExistente(long id, CancellationToken cancellationToken);
}