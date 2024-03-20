namespace OnboardingSIGDB1.Application.Services.Empresa.FindAllEmpresa;

public interface IFindAllEmpresaService
{
    Task<List<Domain.Entities.Empresa>> RetornaEmpresasExistentes(CancellationToken cancellationToken);
}
