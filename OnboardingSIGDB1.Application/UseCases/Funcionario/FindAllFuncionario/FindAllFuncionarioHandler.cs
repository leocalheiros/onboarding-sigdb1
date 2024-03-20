using MediatR;
using OnboardingSIGDB1.Application.Services.Funcionario.FindAllFuncionarioService;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.FindAllFuncionario;

public class FindAllFuncionarioHandler : IRequestHandler<FindAllFuncionarioQuery, List<Domain.Entities.Funcionario>>
{
    private readonly IFindAllFuncionarioService _findAllFuncionarioService;

    public FindAllFuncionarioHandler(IFindAllFuncionarioService findAllFuncionarioService)
    {
        _findAllFuncionarioService = findAllFuncionarioService;
    }
    
    public async Task<List<Domain.Entities.Funcionario>> Handle(FindAllFuncionarioQuery request, CancellationToken cancellationToken)
    {
        
        return await _findAllFuncionarioService.RetornaFuncionariosExistentes(cancellationToken);
    }
}