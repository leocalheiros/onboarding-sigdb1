namespace OnboardingSIGDB1.Application.Services.Empresa.DeleteEmpresaById;

public interface IDeleteEmpresaByIdService
{
    Task<Domain.Entities.Empresa> ChecaValidacoesDeleteEmpresa(long id, CancellationToken cancellationToken);
    Task<Domain.Entities.Empresa> ChecaEmpresaExistente(long id, CancellationToken cancellationToken);
}