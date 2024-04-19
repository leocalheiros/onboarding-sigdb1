using OnboardingSIGDB1.Domain.Dtos;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Entities.Common;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface IFuncionarioRepository : IBaseRepository<Funcionario>
{
    new Task<List<FuncionarioDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<Funcionario> FindByCpfAsync(string cpf, CancellationToken cancellationToken);
    Task<bool> FuncionarioHasEmpresaAsync(long funcionarioId, CancellationToken cancellationToken);
    Task<List<Funcionario>> GetEmpresasComFiltros(FuncionarioFiltro filtro, CancellationToken cancellationToken);
}