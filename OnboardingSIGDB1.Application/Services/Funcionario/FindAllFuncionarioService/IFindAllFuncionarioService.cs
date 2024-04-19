using OnboardingSIGDB1.Domain.Dtos;

namespace OnboardingSIGDB1.Application.Services.Funcionario.FindAllFuncionarioService;

public interface IFindAllFuncionarioService 
{
    Task<List<FuncionarioDto>> RetornaFuncionariosExistentes(CancellationToken cancellationToken);
}