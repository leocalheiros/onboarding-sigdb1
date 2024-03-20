using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Funcionario.DeleteFuncionarioByIdService;

public class DeleteFuncionarioByIdService : IDeleteFuncionarioByIdService
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly NotificationContext _notificationContext;

    public DeleteFuncionarioByIdService(IFuncionarioRepository funcionarioRepository, NotificationContext notificationContext)
    {
        _funcionarioRepository = funcionarioRepository;
        _notificationContext = notificationContext;
    }
    
    public async Task<Domain.Entities.Funcionario> ChecaFuncionarioExistente(long id, CancellationToken cancellationToken)
    {
        var funcionarioExistente = await _funcionarioRepository.GetByAsync(id, cancellationToken);
        if (funcionarioExistente == null)
        {
            _notificationContext.AddNotification("Funcionário não existente/encontrado!");
            return null;
        }

        return funcionarioExistente;
    }
}