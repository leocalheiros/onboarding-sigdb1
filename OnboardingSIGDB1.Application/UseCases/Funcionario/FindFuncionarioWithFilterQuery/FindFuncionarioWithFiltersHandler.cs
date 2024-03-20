using MediatR;
using OnboardingSIGDB1.Application.Services.Funcionario.FindFuncionarioWithFiltersService;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.FindFuncionarioWithFilterQuery;

public class FindFuncionarioWithFiltersHandler : IRequestHandler<FindFuncionarioWithFiltersQuery, List<Domain.Entities.Funcionario>>
{
    private readonly IFindFuncionarioWithFiltersService _service;

    public FindFuncionarioWithFiltersHandler(IFindFuncionarioWithFiltersService service)
    {
        _service = service;
    }
    
    public async Task<List<Domain.Entities.Funcionario>> Handle(FindFuncionarioWithFiltersQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetFuncionariosComFiltros(request.Filtro, cancellationToken);
    }
}