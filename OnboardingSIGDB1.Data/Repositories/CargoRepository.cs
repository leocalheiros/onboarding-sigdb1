using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Persistence;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Data.Repositories;

public class CargoRepository : BaseRepository<Cargo>, ICargoRepository
{
    public CargoRepository(OnboardingSIGContext context) : base(context)
    {
    }
    
    public new async Task<List<Cargo>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Cargos.ToListAsync(cancellationToken);
    }

    public async Task<Cargo> GetByDescricaoAsync(string descricao, CancellationToken cancellationToken)
    {
        return await _context.Cargos.FirstOrDefaultAsync(x => x.Descricao == descricao, cancellationToken);
    }
    
    public async Task<bool> HasFuncionariosVinculadosAsync(long cargoId, CancellationToken cancellationToken)
    {
        return await _context.FuncionarioCargos.AnyAsync(f => f.CargoId == cargoId, cancellationToken);
    }
}