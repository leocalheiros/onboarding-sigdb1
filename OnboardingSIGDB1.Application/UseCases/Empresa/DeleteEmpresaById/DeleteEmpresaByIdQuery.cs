using MediatR;

namespace OnboardingSIGDB1.Application.UseCases.Empresa.DeleteEmpresaById;

public class DeleteEmpresaByIdQuery : IRequest<DeleteEmpresaByIdResult>
{
    public long Id { get; set; }    
}