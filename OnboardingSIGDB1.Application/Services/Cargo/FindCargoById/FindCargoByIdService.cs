using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Cargo.FindCargoById;

public class FindCargoByIdService : IFindCargoByIdService
{
    private readonly ICargoRepository _cargoRepository;
    private readonly NotificationContext _notificationContext;
    
    public FindCargoByIdService(ICargoRepository cargoRepository, NotificationContext notificationContext)
    {
        _cargoRepository = cargoRepository;
        _notificationContext = notificationContext;
    }
    
    public async Task<Domain.Entities.Cargo> ValidaCargoExistente(long id, CancellationToken cancellationToken)
    {
        var cargoExistente = await _cargoRepository.GetByAsync(id, cancellationToken);
        if (cargoExistente == null)
        {
            _notificationContext.AddNotification("Cargo não existente!");
            return null;
        }

        return cargoExistente;
    }
}