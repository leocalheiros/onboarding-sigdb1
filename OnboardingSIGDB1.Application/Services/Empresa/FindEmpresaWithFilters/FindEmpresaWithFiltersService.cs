using OnboardingSIGDB1.Application.Services.Empresa._base;
using OnboardingSIGDB1.Domain.Entities.Common;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Application.Services.Empresa.FindEmpresaWithFilters;

public class FindEmpresaWithFiltersService : BaseEmpresaService, IFindEmpresaWithFiltersService
{
    private readonly IEmpresaRepository _empresaRepository;
    
    public FindEmpresaWithFiltersService(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }
    
    public async Task<List<Domain.Entities.Empresa>> GetEmpresasComFiltros(EmpresaFiltro filtro, CancellationToken cancellationToken)
    {
        if (filtro.Cnpj != null)
        {
            var cnpjCleanned = CleanCnpj(filtro.Cnpj);
            filtro.Cnpj = cnpjCleanned;
        }
        return await _empresaRepository.GetEmpresasComFiltros(filtro, cancellationToken);
    }
}