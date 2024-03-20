using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Application.Data;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Persistence;

public class OnboardingSIGContext : DbContext, IDatabaseContext
{
    
    public OnboardingSIGContext(DbContextOptions<OnboardingSIGContext> options) : base(options)
    {
    }
    
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<FuncionarioCargo> FuncionarioCargos { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
    
}