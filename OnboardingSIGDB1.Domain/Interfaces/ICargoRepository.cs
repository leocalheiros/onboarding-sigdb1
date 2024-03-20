using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface ICargoRepository : IBaseRepository<Cargo>
{
    new Task<List<Cargo>> GetAllAsync(CancellationToken cancellationToken);
    Task<Cargo> GetByDescricaoAsync(string descricao, CancellationToken cancellationToken);
    Task<bool> HasFuncionariosVinculadosAsync(long cargoId, CancellationToken cancellationToken);
}