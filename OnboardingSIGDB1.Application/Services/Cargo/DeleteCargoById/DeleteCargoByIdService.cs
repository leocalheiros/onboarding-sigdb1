using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Cargo.DeleteCargoById;

public class DeleteCargoByIdService : IDeleteCargoByIdService
{
    private readonly ICargoRepository _cargoRepository;
    private readonly NotificationContext _notificationContext;

    public DeleteCargoByIdService(ICargoRepository cargoRepository, NotificationContext notificationContext)
    {
        _cargoRepository = cargoRepository;
        _notificationContext = notificationContext;
    }
    
    public async Task<Domain.Entities.Cargo> ChecaValidacoesDeleteCargo(long id, CancellationToken cancellationToken)
    {
        var cargoExistente = await ChecaCargoExistente(id, cancellationToken);
        if (cargoExistente == null)
        {
            return null;
        }
        
        var temFuncionariosVinculados = await _cargoRepository.HasFuncionariosVinculadosAsync(cargoExistente.Id, cancellationToken);
        if (temFuncionariosVinculados)
        {
            _notificationContext.AddNotification("Não é possível excluir um cargo que tem funcionários vinculados");
            return null;
        }
        return cargoExistente;
    }

    public async Task<Domain.Entities.Cargo> ChecaCargoExistente(long id, CancellationToken cancellationToken)
    {
        var cargoExistente = await _cargoRepository.GetByAsync(id, cancellationToken);
        if (cargoExistente == null)
        {
            _notificationContext.AddNotification("Cargo não encontrado!");
        }

        return cargoExistente;
    }
}