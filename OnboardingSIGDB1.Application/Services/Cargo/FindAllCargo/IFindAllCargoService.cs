namespace OnboardingSIGDB1.Application.Services.Cargo.FindAllCargo;

public interface IFindAllCargoService
{
    Task<List<Domain.Entities.Cargo>> RetornaCargosExistentes(CancellationToken cancellationToken);
}