using MediatR;
using OnboardingSIGDB1.Application.Services.Funcionario.FindAllFuncionarioService;
using OnboardingSIGDB1.Domain.Dtos;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.FindAllFuncionario;

public class FindAllFuncionarioHandler : IRequestHandler<FindAllFuncionarioQuery, List<FuncionarioDto>>
{
    private readonly IFindAllFuncionarioService _findAllFuncionarioService;

    public FindAllFuncionarioHandler(IFindAllFuncionarioService findAllFuncionarioService)
    {
        _findAllFuncionarioService = findAllFuncionarioService;
    }
    
    public async Task<List<FuncionarioDto>> Handle(FindAllFuncionarioQuery request, CancellationToken cancellationToken)
    {
        
        return await _findAllFuncionarioService.RetornaFuncionariosExistentes(cancellationToken);
    }
}