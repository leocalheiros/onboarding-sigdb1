using OnboardingSIGDB1.Domain.Entities.Common;

namespace OnboardingSIGDB1.Application.Services.Empresa.FindEmpresaWithFilters;

public interface IFindEmpresaWithFiltersService
{
    Task<List<Domain.Entities.Empresa>> GetEmpresasComFiltros(EmpresaFiltro filtro, CancellationToken cancellationToken);
}