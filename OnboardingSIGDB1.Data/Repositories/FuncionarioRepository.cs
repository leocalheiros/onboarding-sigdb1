using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Persistence;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Entities.Common;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Data.Repositories;

public class FuncionarioRepository : BaseRepository<Funcionario>, IFuncionarioRepository
{
    public FuncionarioRepository(OnboardingSIGContext context) : base(context)
    {
    }

    public async Task<List<Funcionario>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Funcionarios.ToListAsync(cancellationToken);
    }
    
    public async Task<Funcionario> FindByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        return await _context.Funcionarios.FirstOrDefaultAsync(x => x.Cpf == cpf, cancellationToken);
    }
    
    public async Task<bool> FuncionarioHasEmpresaAsync(long funcionarioId, CancellationToken cancellationToken)
    {
        return await _context.Funcionarios
            .AnyAsync(f => f.Id == funcionarioId && f.EmpresaId != null, cancellationToken);
    }
    
    public async Task<List<Funcionario>> GetEmpresasComFiltros(FuncionarioFiltro filtro, CancellationToken cancellationToken)
    {
        var query = _context.Funcionarios.AsQueryable();

        if (!string.IsNullOrEmpty(filtro.Nome))
        {
            query = query.Where(f => f.Nome.Contains(filtro.Nome));
        }

        if (!string.IsNullOrEmpty(filtro.Cpf))
        {
            query = query.Where(f => f.Cpf == filtro.Cpf);
        }

        if (filtro.DataContratacaoInicio != null && filtro.DataContratacaoFinal != null)
        {
            query = query.Where(f => f.DataContratacao 
                >= filtro.DataContratacaoInicio && f.DataContratacao <= filtro.DataContratacaoFinal);
        }

        return await query.ToListAsync(cancellationToken);
    }
}