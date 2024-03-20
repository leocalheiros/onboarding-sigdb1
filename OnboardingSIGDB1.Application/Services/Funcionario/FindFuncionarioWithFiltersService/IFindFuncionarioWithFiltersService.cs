using OnboardingSIGDB1.Domain.Entities.Common;

namespace OnboardingSIGDB1.Application.Services.Funcionario.FindFuncionarioWithFiltersService;

public interface IFindFuncionarioWithFiltersService
{
    Task<List<Domain.Entities.Funcionario>> GetFuncionariosComFiltros(FuncionarioFiltro filtro, CancellationToken cancellationToken);
}