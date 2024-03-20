namespace OnboardingSIGDB1.Application.Services.Cargo.DeleteCargoById;

public interface IDeleteCargoByIdService
{
    Task<Domain.Entities.Cargo> ChecaValidacoesDeleteCargo(long id, CancellationToken cancellationToken);
    Task<Domain.Entities.Cargo> ChecaCargoExistente(long id, CancellationToken cancellationToken);
}