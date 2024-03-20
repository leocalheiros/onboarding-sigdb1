using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Mappings;

public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
         builder.ToTable("Empresa");
         
         builder
             .HasKey(x => x.Id)
             .HasName("PK_EMPRESA").IsClustered();

         builder
             .Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("BIGINT")
             .IsRequired();
         
            builder
                .Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("VARCHAR(150)");

            builder
                .Property(x => x.Cnpj)
                .IsRequired()
                .HasColumnType("VARCHAR(14)");

            builder
                .Property(x => x.DataFundacao)
                .IsRequired()
                .HasColumnType("DATETIMEOFFSET");
    }
}