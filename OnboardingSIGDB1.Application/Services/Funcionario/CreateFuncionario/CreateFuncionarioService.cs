using OnboardingSIGDB1.Application.Services.Funcionario._base;
using OnboardingSIGDB1.Application.UseCases.Funcionario.CreateFuncionario;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Funcionario.CreateFuncionario;

public class CreateFuncionarioService : BaseFuncionarioService, ICreateFuncionarioService
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly NotificationContext _notificationContext;
    
    public CreateFuncionarioService(IFuncionarioRepository funcionarioRepository, NotificationContext notificationContext)
    {
        _funcionarioRepository = funcionarioRepository;
        _notificationContext = notificationContext;
    }
    
    public async Task ChecaValidacoesCpf(CreateFuncionarioCommand command, CancellationToken cancellationToken)
    {
        string cpfCleanned = CleanCpf(command.Cpf);
        var funcionarioExistente = await _funcionarioRepository.FindByCpfAsync(cpfCleanned, cancellationToken);
        if (funcionarioExistente != null)
        {
            _notificationContext.AddNotification("Já existe um funcionário com esse CPF cadastrado!");
            return;
        }

        if (!CpfIsValid(cpfCleanned))
        {
            _notificationContext.AddNotification("O CPF inserido é inválido!");
            return;
        }

        command.Cpf = cpfCleanned;
    }
}