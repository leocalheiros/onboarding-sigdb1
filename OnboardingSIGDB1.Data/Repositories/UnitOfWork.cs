using OnboardingSIGDB1.Data.Persistence;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly OnboardingSIGContext _context;

    public UnitOfWork(OnboardingSIGContext context)
    {
        _context = context;
    }
    
    public async Task Commit(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}