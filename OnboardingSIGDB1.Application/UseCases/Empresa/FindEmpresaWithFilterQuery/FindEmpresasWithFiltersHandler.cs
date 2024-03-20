using MediatR;
using OnboardingSIGDB1.Application.Services.Empresa.FindEmpresaWithFilters;
using OnboardingSIGDB1.Application.UseCases.FindEmpresaWithFilterQuery;

namespace OnboardingSIGDB1.Application.UseCases.Empresa.FindEmpresaWithFilterQuery;

public class FindEmpresasWithFiltersHandler : IRequestHandler<FindEmpresasWithFiltersQuery, List<Domain.Entities.Empresa>>
{
    private readonly IFindEmpresaWithFiltersService _service;

    public FindEmpresasWithFiltersHandler(IFindEmpresaWithFiltersService service)
    {
        _service = service;
    }
    
    public async Task<List<Domain.Entities.Empresa>> Handle(FindEmpresasWithFiltersQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetEmpresasComFiltros(request.Filtro, cancellationToken);
    }
}