using OnboardingSIGDB1.Application.Services.Funcionario._base;
using OnboardingSIGDB1.Domain.Entities.Common;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Funcionario.FindFuncionarioWithFiltersService;

public class FindFuncionarioWithFiltersService : BaseFuncionarioService, IFindFuncionarioWithFiltersService
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public FindFuncionarioWithFiltersService(IFuncionarioRepository funcionarioRepository, NotificationContext notificationContext)
    {
        _funcionarioRepository = funcionarioRepository;
    }
    
    public Task<List<Domain.Entities.Funcionario>> GetFuncionariosComFiltros(FuncionarioFiltro filtro, CancellationToken cancellationToken)
    {
        if (filtro.Cpf != null)
        {
            var cpfCleanned = CleanCpf(filtro.Cpf);
            filtro.Cpf = cpfCleanned;
        }

        return _funcionarioRepository.GetEmpresasComFiltros(filtro, cancellationToken);
    }
}