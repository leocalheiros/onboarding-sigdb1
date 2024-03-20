using OnboardingSIGDB1.Application.UseCases.Funcionario.CreateFuncionario;

namespace OnboardingSIGDB1.Application.Services.Funcionario.CreateFuncionario;

public interface ICreateFuncionarioService
{
    Task ChecaValidacoesCpf(CreateFuncionarioCommand command, CancellationToken cancellationToken);
}