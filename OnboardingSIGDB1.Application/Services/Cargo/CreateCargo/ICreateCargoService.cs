using OnboardingSIGDB1.Application.UseCases.Cargo;

namespace OnboardingSIGDB1.Application.Services.Cargo.CreateCargo;

public interface ICreateCargoService
{
    Task ChecaValidacoesCargo(CreateCargoCommand command, CancellationToken cancellationToken);
}