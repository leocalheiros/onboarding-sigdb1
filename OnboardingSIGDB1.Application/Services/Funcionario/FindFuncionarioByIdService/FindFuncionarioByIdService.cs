using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Funcionario.FindFuncionarioByIdService;

public class FindFuncionarioByIdService : IFindFuncionarioByIdService
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly NotificationContext _notificationContext;

    public FindFuncionarioByIdService(IFuncionarioRepository funcionarioRepository, NotificationContext notificationContext)
    {
        _funcionarioRepository = funcionarioRepository;
        _notificationContext = notificationContext;
    }
    
    public async Task<Domain.Entities.Funcionario> ValidaFuncionarioExistente(long id, CancellationToken cancellationToken)
    {
        var funcionario = await _funcionarioRepository.GetByAsync(id, cancellationToken);
        if (funcionario == null)
        {
            _notificationContext.AddNotification("Funcionário não existente/encontrado!");
            return null;
        }

        return funcionario;
    }
}