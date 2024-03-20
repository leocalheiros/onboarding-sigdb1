using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Persistence;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Entities.Common;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Data.Repositories;

public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
{
    public EmpresaRepository(OnboardingSIGContext context) : base(context)
    { }

    public new async Task<List<Empresa>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Empresas.ToListAsync(cancellationToken);
    }

    public async Task<Empresa> FindByCnpjAsync(string cnpj, CancellationToken cancellationToken)
    {
        return await _context.Empresas.FirstOrDefaultAsync(x => x.Cnpj == cnpj, cancellationToken);
    }
    
    public async Task<List<Empresa>> GetEmpresasComFiltros(EmpresaFiltro filtro, CancellationToken cancellationToken)
    {
        var query = _context.Empresas.AsQueryable();

        if (!string.IsNullOrEmpty(filtro.Nome))
        {
            query = query.Where(e => e.Nome.Contains(filtro.Nome));
        }

        if (!string.IsNullOrEmpty(filtro.Cnpj))
        {
            query = query.Where(e => e.Cnpj == filtro.Cnpj);
        }

        if (filtro.DataFundacaoInicio != null && filtro.DataFundacaoFim != null)
        {
            query = query.Where(e => e.DataFundacao >= filtro.DataFundacaoInicio && e.DataFundacao <= filtro.DataFundacaoFim);
        }

        return await query.ToListAsync(cancellationToken);
    }
    
    public async Task<bool> HasFuncionariosVinculadosAsync(long empresaId, CancellationToken cancellationToken)
    {
        return await _context.Funcionarios.AnyAsync(f => f.EmpresaId == empresaId, cancellationToken);
    }
    
}