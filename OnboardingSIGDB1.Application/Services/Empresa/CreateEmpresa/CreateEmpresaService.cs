using OnboardingSIGDB1.Application.Services.Empresa._base;
using OnboardingSIGDB1.Application.UseCases.Empresa.CreateEmpresa;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Empresa.CreateEmpresa;

public class CreateEmpresaService: BaseEmpresaService, ICreateEmpresaService
{
    private readonly IEmpresaRepository _empresaRepository;
    private readonly NotificationContext _notificationContext;
    
    public CreateEmpresaService(IEmpresaRepository empresaRepository, NotificationContext notificationContext)
    {
        _empresaRepository = empresaRepository;
        _notificationContext = notificationContext;
    }
    
    public async Task ChecaValidacoesCnpj(CreateEmpresaCommand command, CancellationToken cancellationToken)
    {
        
        string cnpjCleaned = CleanCnpj(command.Cnpj);
        
        var empresaExistente = await _empresaRepository.FindByCnpjAsync(cnpjCleaned, cancellationToken);
        if (empresaExistente != null)
        {
            _notificationContext.AddNotification("Já existe uma empresa cadastrada com este CNPJ!");
            return;
        }

        if (!CnpjIsValid(cnpjCleaned))
        {
            _notificationContext.AddNotification("O CNPJ informado não é valido!");
            return;
        }

        command.Cnpj = cnpjCleaned;
    }
}