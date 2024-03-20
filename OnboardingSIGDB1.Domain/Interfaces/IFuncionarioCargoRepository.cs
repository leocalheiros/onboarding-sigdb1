using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface IFuncionarioCargoRepository 
{
    Task CreateAsync(FuncionarioCargo entity, CancellationToken cancellationToken);
    Task<List<long>> GetCargoIdsByFuncionarioIdAsync(long funcionarioId, CancellationToken cancellationToken);
}