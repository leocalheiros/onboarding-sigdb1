using OnboardingSIGDB1.Application.UseCases.Empresa.CreateEmpresa;

namespace OnboardingSIGDB1.Application.Services.Empresa.CreateEmpresa;

public interface ICreateEmpresaService
{
    Task ChecaValidacoesCnpj(CreateEmpresaCommand command, CancellationToken cancellationToken);
}