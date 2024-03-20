using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Empresa.DeleteEmpresaById;

public class DeleteEmpresaByIdService : IDeleteEmpresaByIdService
{
    private readonly IEmpresaRepository _empresaRepository;
    private readonly NotificationContext _notificationContext;

    public DeleteEmpresaByIdService(IEmpresaRepository empresaRepository, NotificationContext notificationContext)
    {
        _empresaRepository = empresaRepository;
        _notificationContext = notificationContext;
    }
    
    public async Task<Domain.Entities.Empresa> ChecaValidacoesDeleteEmpresa(long id, CancellationToken cancellationToken)
    {
        var empresaExistente = await ChecaEmpresaExistente(id, cancellationToken);
        if (empresaExistente == null)
        {
            return null;
        }
        
        
        var temFuncionariosVinculados = await _empresaRepository.HasFuncionariosVinculadosAsync(id, cancellationToken);
        if (temFuncionariosVinculados)
        {
            _notificationContext.AddNotification("Não é possível excluir a empresa, pois há funcionários vinculados a ela.");
            return null;
        }
        return empresaExistente;
    }
    
    public async Task<Domain.Entities.Empresa> ChecaEmpresaExistente(long id, CancellationToken cancellationToken)
    {
        var empresaExistente = await _empresaRepository.GetByAsync(id, cancellationToken);
        if (empresaExistente == null)
        {
            _notificationContext.AddNotification("Empresa não encontrada!");
        }

        return empresaExistente;
    }
}