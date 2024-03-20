using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Mappings
{
    public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable("Funcionario");

            builder
                .HasKey(x => x.Id)
                .HasName("PK_FUNCIONARIO")
                .IsClustered();

            builder
                .Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("BIGINT")
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("VARCHAR(150)");

            builder
                .Property(x => x.Cpf)
                .IsRequired()
                .HasColumnType("VARCHAR(11)");

            builder
                .Property(x => x.DataContratacao)
                .IsRequired()
                .HasColumnType("DATETIMEOFFSET");

            // Relacionamento com Empresa
            builder.HasOne(x => x.Empresa)
                .WithMany(e => e.Funcionarios)
                .HasForeignKey(x => x.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}