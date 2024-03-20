using OnboardingSIGDB1.Application.UseCases.FindEmpresaById;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Empresa.FindEmpresaById;

public class FindEmpresaByIdService : IFindEmpresaByIdService
{
    private readonly IEmpresaRepository _empresaRepository;
    private readonly NotificationContext _notificationContext;
    
    public FindEmpresaByIdService(IEmpresaRepository empresaRepository, NotificationContext notificationContext)
    {
        _empresaRepository = empresaRepository;
        _notificationContext = notificationContext;
    }
    
    public async Task<Domain.Entities.Empresa> ValidaEmpresaExistente(long id, CancellationToken cancellationToken)
    {
        var empresa = await _empresaRepository.GetByAsync(id, cancellationToken);
        if (empresa == null)
        {
            _notificationContext.AddNotification("Empresa não encontrada/inexistente");
            return null;
        }

        return empresa;
    }
}