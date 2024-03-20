using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Mappings;

public class CargoMapping : IEntityTypeConfiguration<Cargo>
{
    public void Configure(EntityTypeBuilder<Cargo> builder)
    {
        builder.ToTable("Cargo");

        builder
            .HasKey(x => x.Id)
            .HasName("PK_CARGO")
            .IsClustered();

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("BIGINT");

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnType("VARCHAR(250)");
    }
}