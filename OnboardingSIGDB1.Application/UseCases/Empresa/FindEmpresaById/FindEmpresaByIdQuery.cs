using MediatR;

namespace OnboardingSIGDB1.Application.UseCases.FindEmpresaById;

public class FindEmpresaByIdQuery : IRequest<FindEmpresaByIdResult>
{
    public long Id { get; set; }
}