namespace OnboardingSIGDB1.Application.Services.Funcionario.FindAllFuncionarioService;

public interface IFindAllFuncionarioService 
{
    Task<List<Domain.Entities.Funcionario>> RetornaFuncionariosExistentes(CancellationToken cancellationToken);
}