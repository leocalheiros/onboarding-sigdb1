using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Entities.Common;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface IEmpresaRepository : IBaseRepository<Empresa>
{
    new Task<List<Empresa>> GetAllAsync(CancellationToken cancellationToken);
    Task<Empresa> FindByCnpjAsync(string cnpj, CancellationToken cancellationToken);
    Task<List<Empresa>> GetEmpresasComFiltros(EmpresaFiltro filtro, CancellationToken cancellationToken);
    Task<bool> HasFuncionariosVinculadosAsync(long empresaId, CancellationToken cancellationToken);
}