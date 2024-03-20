using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Persistence;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Data.Repositories;

public class FuncionarioCargoRepository : IFuncionarioCargoRepository
{
    protected readonly OnboardingSIGContext _context;

    public FuncionarioCargoRepository(OnboardingSIGContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(FuncionarioCargo entity, CancellationToken cancellationToken)
    {
        await _context.AddAsync(entity, cancellationToken);
    }
    
    public async Task<List<long>> GetCargoIdsByFuncionarioIdAsync(long funcionarioId, CancellationToken cancellationToken)
    {
        return await _context.FuncionarioCargos
            .Where(fc => fc.FuncionarioId == funcionarioId)
            .Select(fc => fc.CargoId)
            .ToListAsync(cancellationToken);
    }
    
}