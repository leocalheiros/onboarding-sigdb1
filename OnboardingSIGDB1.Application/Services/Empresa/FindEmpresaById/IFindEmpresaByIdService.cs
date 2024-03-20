using OnboardingSIGDB1.Application.UseCases.FindEmpresaById;

namespace OnboardingSIGDB1.Application.Services.Empresa.FindEmpresaById;

public interface IFindEmpresaByIdService
{
    Task<Domain.Entities.Empresa> ValidaEmpresaExistente(long id,
        CancellationToken cancellationToken);
}