using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Application.Data;

public interface IDatabaseContext
{
    DbSet<Empresa> Empresas { get; set; }
    DbSet<Funcionario> Funcionarios { get; set; }
    DbSet<FuncionarioCargo> FuncionarioCargos { get; set; }
    DbSet<Cargo> Cargos { get; set; }
}