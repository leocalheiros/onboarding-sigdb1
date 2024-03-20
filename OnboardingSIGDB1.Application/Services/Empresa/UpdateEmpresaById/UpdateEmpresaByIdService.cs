using OnboardingSIGDB1.Application.Services.Empresa._base;
using OnboardingSIGDB1.Application.UseCases.Empresa._base;
using OnboardingSIGDB1.Application.UseCases.Empresa.UpdateEmpresaById;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Empresa.UpdateEmpresaById;

public class UpdateEmpresaByIdService : BaseEmpresaService, IUpdateEmpresaByIdService
{
    private readonly IEmpresaRepository _empresaRepository;
    private readonly NotificationContext _notificationContext;

    public UpdateEmpresaByIdService(IEmpresaRepository empresaRepository, NotificationContext notificationContext)
    {
        _empresaRepository = empresaRepository;
        _notificationContext = notificationContext;
    }

    public async Task<Domain.Entities.Empresa> ChecaValidacoesUpdate(long id,
        UpdateEmpresaByIdCommand command,
        CancellationToken cancellationToken)
    {
        var empresaExistente = await ChecaEmpresaExistente(id, cancellationToken);
        if (empresaExistente == null)
        {
            return null;
        }
        
        var cnpjValidated = ValidaCnpj(command.Cnpj, cancellationToken);
        if (cnpjValidated == null)
        {
            return null;
        }
        
        var empresa = await _empresaRepository.FindByCnpjAsync(cnpjValidated, cancellationToken);
        if (empresa != null)
        {
            _notificationContext.AddNotification("CNPJ já existente!");
            return null;
        }
        
        command.Cnpj = cnpjValidated;
        empresaExistente.AlterarTodosDados(command.Nome, cnpjValidated, command.DataFundacao);
        return empresaExistente;
    }
    

    public async Task<Domain.Entities.Empresa> ChecaEmpresaExistente(long id, CancellationToken cancellationToken)
    {
        var empresaExistente = await _empresaRepository.GetByAsync(id, cancellationToken);
        if (empresaExistente == null)
        {
            _notificationContext.AddNotification("Empresa não existente/incorreta para atualização");
        }

        return empresaExistente;
    }
    
    public string ValidaCnpj(string cnpj, CancellationToken cancellationToken)
    {
        var cnpjCleanned = CleanCnpj(cnpj);
        if (!CnpjIsValid(cnpjCleanned))
        {
            _notificationContext.AddNotification("O CNPJ informado não é valido!");
        }

        return cnpjCleanned;
    }

    public async Task ChecaCnpjExistente(string cnpj, CancellationToken cancellationToken)
    {
        var empresa = await _empresaRepository.FindByCnpjAsync(cnpj, cancellationToken);
        if (empresa != null)
        {
            _notificationContext.AddNotification("CNPJ já existente!");
        }
    }
    
}