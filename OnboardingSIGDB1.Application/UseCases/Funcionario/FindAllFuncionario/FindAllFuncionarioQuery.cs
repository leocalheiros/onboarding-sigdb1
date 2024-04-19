using MediatR;
using OnboardingSIGDB1.Domain.Dtos;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.FindAllFuncionario;

public class FindAllFuncionarioQuery : IRequest<List<FuncionarioDto>>
{
    
}