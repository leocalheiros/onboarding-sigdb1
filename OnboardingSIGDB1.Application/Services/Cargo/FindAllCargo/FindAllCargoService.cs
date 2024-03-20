using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Application.Services.Cargo.FindAllCargo;

public class FindAllCargoService : IFindAllCargoService
{
    private readonly ICargoRepository _cargoRepository;
    
    public FindAllCargoService(ICargoRepository cargoRepository)
    {
        _cargoRepository = cargoRepository;
    }
    
    public async Task<List<Domain.Entities.Cargo>> RetornaCargosExistentes(CancellationToken cancellationToken)
    {
        var cargosExistentes = await _cargoRepository.GetAllAsync(cancellationToken);
        if (cargosExistentes == null)
        {
            throw new Exception("Não há cargos existentes!");
        }

        return cargosExistentes;
    }
}