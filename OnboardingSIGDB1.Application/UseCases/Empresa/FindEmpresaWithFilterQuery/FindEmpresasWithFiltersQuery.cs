using MediatR;
using OnboardingSIGDB1.Domain.Entities.Common;

namespace OnboardingSIGDB1.Application.UseCases.FindEmpresaWithFilterQuery;

public class FindEmpresasWithFiltersQuery : IRequest<List<Domain.Entities.Empresa>>
{
    public EmpresaFiltro Filtro { get; set; }
}