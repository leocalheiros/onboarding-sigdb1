using OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioEmpresaId;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Funcionario.VincularFuncionarioEmpresaIdService;

public class VincularFuncionarioEmpresaIdService : IVincularFuncionarioEmpresaIdService
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly IEmpresaRepository _empresaRepository;
    private readonly NotificationContext _notificationContext;

    public VincularFuncionarioEmpresaIdService(IFuncionarioRepository funcionarioRepository,
        IEmpresaRepository empresaRepository,
        NotificationContext notificationContext)
    {
        _funcionarioRepository = funcionarioRepository;
        _empresaRepository = empresaRepository;
        _notificationContext = notificationContext;
    }
    
    public async Task<Domain.Entities.Funcionario> VincularFuncionarioEmpresaAsync(VincularFuncionarioEmpresaIdCommand command,
        CancellationToken cancellationToken)
    {
        var funcionario = await ObterFuncionarioAsync(command.FuncionarioId, cancellationToken);
        var empresa = await ObterEmpresaAsync(command.EmpresaId, cancellationToken);

        if (funcionario == null || empresa == null)
        {
            return null;
        }

        if (funcionario.EmpresaId != null)
        {
            _notificationContext.AddNotification("Funcionário já vinculado a uma empresa!");
            return null;
        }

        funcionario.AlterarEmpresaId(command.EmpresaId);
        
        return funcionario;
    }

    private async Task<Domain.Entities.Funcionario> ObterFuncionarioAsync(long funcionarioId, CancellationToken cancellationToken)
    {
        var funcionario = await _funcionarioRepository.GetByAsync(funcionarioId, cancellationToken);
        if (funcionario == null)
        {
            _notificationContext.AddNotification("Funcionário não encontrado!");
        }
        return funcionario;
    }

    private async Task<Domain.Entities.Empresa> ObterEmpresaAsync(long empresaId, CancellationToken cancellationToken)
    {
        var empresa = await _empresaRepository.GetByAsync(empresaId, cancellationToken);
        if (empresa == null)
        {
            _notificationContext.AddNotification("Empresa não encontrada/inexistente!");
        }
        return empresa;
    }
}