using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Funcionario.FindAllFuncionarioService;

public class FindAllFuncionarioService : IFindAllFuncionarioService
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly NotificationContext _notificationContext;

    public FindAllFuncionarioService(IFuncionarioRepository funcionarioRepository, NotificationContext notificationContext)
    {
        _funcionarioRepository = funcionarioRepository;
        _notificationContext = notificationContext;
    }
    
    public Task<List<Domain.Entities.Funcionario>> RetornaFuncionariosExistentes(CancellationToken cancellationToken)
    {
        var funcionarios = _funcionarioRepository.GetAllAsync(cancellationToken);
        if (funcionarios == null)
        {
            throw new Exception("Funcionários não encontrados!");
        }

        return funcionarios;
    }
}