using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Persistence;
using OnboardingSIGDB1.Domain.Entities.Common;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly OnboardingSIGContext _context;

    public BaseRepository(OnboardingSIGContext context)
    {
        _context = context;
    }
    
    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _context.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Remove(entity);
    }
    

    public async Task<TEntity> GetByAsync(long id, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }
}