using MediatR;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.DeleteFuncionarioById;

public class DeleteFuncionarioByIdQuery: IRequest<DeleteFuncionarioByIdResult>
{
    public long Id { get; set; }    
}