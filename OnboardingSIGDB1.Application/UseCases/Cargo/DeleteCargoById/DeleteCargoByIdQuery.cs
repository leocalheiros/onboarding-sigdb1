using MediatR;
using OnboardingSIGDB1.Application.UseCases.Funcionario.DeleteFuncionarioById;

namespace OnboardingSIGDB1.Application.UseCases.Cargo.DeleteCargoById;

public class DeleteCargoByIdQuery : IRequest<DeleteCargoByIdResult>
{
    public long Id { get; set; }
}