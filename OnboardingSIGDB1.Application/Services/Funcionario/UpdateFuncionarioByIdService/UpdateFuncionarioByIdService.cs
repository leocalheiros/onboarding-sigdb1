using OnboardingSIGDB1.Application.Services.Funcionario._base;
using OnboardingSIGDB1.Application.UseCases.Funcionario.UpdateFuncionarioById;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Funcionario.UpdateFuncionarioByIdService;

public class UpdateFuncionarioByIdService : BaseFuncionarioService, IUpdateFuncionarioByIdService
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly NotificationContext _notificationContext;

    public UpdateFuncionarioByIdService(IFuncionarioRepository funcionarioRepository, NotificationContext notificationContext)
    {
        _funcionarioRepository = funcionarioRepository;
        _notificationContext = notificationContext;
    }
    
    public async Task<Domain.Entities.Funcionario> ChecaValidacoesUpdate(long id,
        UpdateFuncionarioByIdCommand command,
        CancellationToken cancellationToken)
    {
        var funcionarioExistente = await ObterFuncionarioExistenteAsync(id, cancellationToken);
        if (funcionarioExistente == null)
        {
            return null;
        }
        
        var cpfCleanned = CleanCpf(command.Cpf);

        if (!CpfIsValid(cpfCleanned))
        {
            _notificationContext.AddNotification("CPF inválido!");
            return null;
        }

        var possibleFuncionarioByNewCpf = await ObterFuncionarioPorCpfAsync(funcionarioExistente.Cpf, cpfCleanned, cancellationToken);
        if (possibleFuncionarioByNewCpf != null & cpfCleanned != funcionarioExistente.Cpf)
        {
            return null;
        }
        
        command.Cpf = cpfCleanned;
        funcionarioExistente.AlterarTodosDadosFuncionario(command.Nome, command.Cpf, command.DataContratacao);

        return funcionarioExistente;
    }

    private async Task<Domain.Entities.Funcionario> ObterFuncionarioExistenteAsync(long id, CancellationToken cancellationToken)
    {
        var funcionarioExistente = await _funcionarioRepository.GetByAsync(id, cancellationToken);
        if (funcionarioExistente == null)
        {
            _notificationContext.AddNotification("Funcionário não existente/encontrado!");
        }
        return funcionarioExistente;
    }

    private async Task<Domain.Entities.Funcionario> ObterFuncionarioPorCpfAsync(string cpf, string cpfCleanned, CancellationToken cancellationToken)
    {
        var possibleFuncionarioByNewCpf = await _funcionarioRepository.FindByCpfAsync(cpf, cancellationToken);
        if (possibleFuncionarioByNewCpf != null & cpfCleanned != cpf)
        {
            _notificationContext.AddNotification("Conta com esse CPF já existente!");
        }
        return possibleFuncionarioByNewCpf;
    }
}