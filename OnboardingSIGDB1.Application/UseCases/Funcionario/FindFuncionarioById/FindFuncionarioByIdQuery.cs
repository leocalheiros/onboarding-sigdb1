using MediatR;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.FindFuncionarioById;

public class FindFuncionarioByIdQuery : IRequest<FindFuncionarioByIdResult>
{
    public long Id { get; set; }
}