using MediatR;
using OnboardingSIGDB1.Domain.Entities.Common;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.FindFuncionarioWithFilterQuery;

public class FindFuncionarioWithFiltersQuery : IRequest<List<Domain.Entities.Funcionario>>
{
    public FuncionarioFiltro Filtro { get; set; }
}