using OnboardingSIGDB1.Application.UseCases.Cargo;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Cargo.CreateCargo;

public class CreateCargoService : ICreateCargoService
{
    private readonly ICargoRepository _cargoRepository;
    private readonly NotificationContext _notificationContext;
    
    public CreateCargoService(ICargoRepository cargoRepository, NotificationContext notificationContext)
    {
        _cargoRepository = cargoRepository;
        _notificationContext = notificationContext;
    }
    
    public async Task ChecaValidacoesCargo(CreateCargoCommand command, CancellationToken cancellationToken)
    {
        var cargoExistente = await _cargoRepository.GetByDescricaoAsync(command.Descricao, cancellationToken);
        if (cargoExistente != null)
        {
            _notificationContext.AddNotification("Cargo com descrição já existente!");
            return;
        }
    }
}