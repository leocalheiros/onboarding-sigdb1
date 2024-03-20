using OnboardingSIGDB1.Application.UseCases.Empresa.UpdateEmpresaById;

namespace OnboardingSIGDB1.Application.Services.Empresa.UpdateEmpresaById;

public interface IUpdateEmpresaByIdService
{
    Task<Domain.Entities.Empresa> ChecaValidacoesUpdate(long id, UpdateEmpresaByIdCommand command, CancellationToken cancellationToken);
    Task<Domain.Entities.Empresa> ChecaEmpresaExistente(long id, CancellationToken cancellationToken);
    string ValidaCnpj(string cnpj, CancellationToken cancellationToken);
}