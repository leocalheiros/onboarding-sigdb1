using OnboardingSIGDB1.Application.UseCases.Cargo.UpdateCargoById;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.Services.Cargo.UpdateCargoById;

public class UpdateCargoByIdService : IUpdateCargoByIdService
{
    private readonly ICargoRepository _cargoRepository;
    private readonly NotificationContext _notificationContext;

    public UpdateCargoByIdService(ICargoRepository cargoRepository, NotificationContext notificationContext)
    {
        _cargoRepository = cargoRepository;
        _notificationContext = notificationContext;
    }
    
    public async Task<Domain.Entities.Cargo> ChecaValidacoesUpdate(long id, UpdateCargoByIdCommand command, CancellationToken cancellationToken)
    {
        var cargoExistente = await ChecaCargoByIdExistente(id, cancellationToken);
        if (cargoExistente == null)
        {
            return null;
        }
        var descricaoExistente = await _cargoRepository.GetByDescricaoAsync(command.Descricao, cancellationToken);
        if (descricaoExistente != null)
        {
            _notificationContext.AddNotification("Descrição de cargo já existente!");
        }
        
        cargoExistente.AlteraDescricao(command.Descricao);
        return cargoExistente;
    }

    public async Task<Domain.Entities.Cargo> ChecaCargoByIdExistente(long id, CancellationToken cancellationToken)
    {
        var cargoExistente = await _cargoRepository.GetByAsync(id, cancellationToken);
        if (cargoExistente == null)
        {
            _notificationContext.AddNotification("Cargo com ID não encotrado/inexistente!");
        }

        return cargoExistente;
    }
   
}