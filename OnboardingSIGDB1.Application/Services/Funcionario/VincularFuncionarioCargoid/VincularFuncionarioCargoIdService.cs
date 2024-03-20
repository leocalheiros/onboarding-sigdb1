using OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioCargoId;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Funcionario.VincularFuncionarioCargoid;

public class VincularFuncionarioCargoIdService : IVincularFuncionarioCargoIdService
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly IFuncionarioCargoRepository _funcionarioCargoRepository;
    private readonly ICargoRepository _cargoRepository;
    private readonly NotificationContext _notificationContext;

    public VincularFuncionarioCargoIdService(IFuncionarioRepository funcionarioRepository,
        IFuncionarioCargoRepository funcionarioCargoRepository,
        ICargoRepository cargoRepository,
        NotificationContext notificationContext)
    {
        _funcionarioRepository = funcionarioRepository;
        _funcionarioCargoRepository = funcionarioCargoRepository;
        _cargoRepository = cargoRepository;
        _notificationContext = notificationContext;
    }

    public async Task<FuncionarioCargo> VincularFuncionarioCargoIdAsync(
        VincularFuncionarioCargoIdCommand command,
        CancellationToken cancellationToken)
    {
        var funcionario = await ObterFuncionarioAsync(command.FuncionarioId, cancellationToken);
        var cargo = await ObterCargoAsync(command.CargoId, cancellationToken);

        if (funcionario == null || cargo == null)
        {
            return null;
        }

        if (!ValidarVinculoEmpresa(funcionario))
        {
            return null;
        }

        if (await ValidarCargoRepetido(funcionario, command.CargoId, cancellationToken))
        {
            return null;
        }

        var funcionarioCargo = new FuncionarioCargo(funcionario.Id, cargo.Id, command.DataVinculo);
        
        funcionario.AdicionarCargo(funcionarioCargo);
        await _funcionarioCargoRepository.CreateAsync(funcionarioCargo, cancellationToken);

        return funcionarioCargo;
    }

    private async Task<Domain.Entities.Funcionario> ObterFuncionarioAsync(long funcionarioId, CancellationToken cancellationToken)
    {
        var funcionario = await _funcionarioRepository.GetByAsync(funcionarioId, cancellationToken);
        if (funcionario == null)
        {
            _notificationContext.AddNotification("Funcionário não encontrado/existente!");
        }
        return funcionario;
    }

    private async Task<Domain.Entities.Cargo> ObterCargoAsync(long cargoId, CancellationToken cancellationToken)
    {
        var cargo = await _cargoRepository.GetByAsync(cargoId, cancellationToken);
        if (cargo == null)
        {
            _notificationContext.AddNotification("Cargo não encontrado/existente!");
        }
        return cargo;
    }

    private bool ValidarVinculoEmpresa(Domain.Entities.Funcionario funcionario)
    {
        if (funcionario.EmpresaId == null)
        {
            _notificationContext.AddNotification("O funcionário não está vinculado a uma empresa!");
            return false;
        }
        return true;
    }

    private async Task<bool> ValidarCargoRepetido(Domain.Entities.Funcionario funcionario, long cargoId, CancellationToken cancellationToken)
    {
        var cargosFuncionario = await _funcionarioCargoRepository.GetCargoIdsByFuncionarioIdAsync(funcionario.Id, cancellationToken);
        if (cargosFuncionario.Contains(cargoId))
        {
            _notificationContext.AddNotification("O funcionário já possui esse cargo!");
            return true;
        }
        return false;
    }

}