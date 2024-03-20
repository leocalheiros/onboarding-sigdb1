namespace OnboardingSIGDB1.Application.Services.Cargo.FindCargoById;

public interface IFindCargoByIdService
{
    Task<Domain.Entities.Cargo> ValidaCargoExistente(long id,
        CancellationToken cancellationToken);
}