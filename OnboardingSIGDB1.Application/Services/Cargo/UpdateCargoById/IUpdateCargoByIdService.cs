using OnboardingSIGDB1.Application.UseCases.Cargo.UpdateCargoById;

namespace OnboardingSIGDB1.Application.Services.Cargo.UpdateCargoById;

public interface IUpdateCargoByIdService
{
    Task<Domain.Entities.Cargo> ChecaValidacoesUpdate(long id, UpdateCargoByIdCommand command, CancellationToken cancellationToken);
    Task<Domain.Entities.Cargo> ChecaCargoByIdExistente(long id, CancellationToken cancellationToken);
}