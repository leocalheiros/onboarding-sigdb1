using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Mappings;

public class FuncionarioCargoMapping : IEntityTypeConfiguration<FuncionarioCargo>
{
    public void Configure(EntityTypeBuilder<FuncionarioCargo> builder)
    {
        builder.ToTable("FuncionarioCargos");
        
        builder
            .HasKey(x => new { x.FuncionarioId, x.CargoId })
            .HasName("PK_FUNCIONARIO_CARGO");

        builder.Property(x => x.DataVinculo)
            .IsRequired()
            .HasColumnName("DataVinculo")
            .HasColumnType("DATETIMEOFFSET");

        builder.HasOne(x => x.Funcionario)
            .WithMany(f => f.CargosFuncionario)
            .HasForeignKey(x => x.FuncionarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Cargo)
            .WithMany()
            .HasForeignKey(x => x.CargoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}