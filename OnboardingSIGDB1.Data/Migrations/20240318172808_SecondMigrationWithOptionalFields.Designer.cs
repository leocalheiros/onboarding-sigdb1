﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnboardingSIGDB1.Data.Persistence;

#nullable disable

namespace OnboardingSIGDB1.Data.Migrations
{
    [DbContext(typeof(OnboardingSIGContext))]
    [Migration("20240318172808_SecondMigrationWithOptionalFields")]
    partial class SecondMigrationWithOptionalFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.Cargo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("VARCHAR(250)");

                    b.HasKey("Id")
                        .HasName("PK_CARGO");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.ToTable("Cargo", (string)null);
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.Empresa", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("VARCHAR(14)");

                    b.Property<DateTimeOffset>("DataFundacao")
                        .HasColumnType("DATETIMEOFFSET");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(150)");

                    b.HasKey("Id")
                        .HasName("PK_EMPRESA");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.ToTable("Empresa", (string)null);
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.Funcionario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("VARCHAR(11)");

                    b.Property<DateTimeOffset>("DataContratacao")
                        .HasColumnType("DATETIMEOFFSET");

                    b.Property<long?>("EmpresaId")
                        .HasColumnType("BIGINT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(150)");

                    b.HasKey("Id")
                        .HasName("PK_FUNCIONARIO");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.HasIndex("EmpresaId");

                    b.ToTable("Funcionario", (string)null);
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.FuncionarioCargo", b =>
                {
                    b.Property<long>("FuncionarioId")
                        .HasColumnType("BIGINT");

                    b.Property<long>("CargoId")
                        .HasColumnType("BIGINT");

                    b.Property<DateTimeOffset>("DataVinculo")
                        .HasColumnType("DATETIMEOFFSET")
                        .HasColumnName("DataVinculo");

                    b.HasKey("FuncionarioId", "CargoId")
                        .HasName("PK_FUNCIONARIO_CARGO");

                    b.HasIndex("CargoId");

                    b.ToTable("FuncionarioCargos", (string)null);
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.Funcionario", b =>
                {
                    b.HasOne("OnboardingSIGDB1.Domain.Entities.Empresa", "Empresa")
                        .WithMany("Funcionarios")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.FuncionarioCargo", b =>
                {
                    b.HasOne("OnboardingSIGDB1.Domain.Entities.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OnboardingSIGDB1.Domain.Entities.Funcionario", "Funcionario")
                        .WithMany("CargosFuncionario")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.Empresa", b =>
                {
                    b.Navigation("Funcionarios");
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.Funcionario", b =>
                {
                    b.Navigation("CargosFuncionario");
                });
#pragma warning restore 612, 618
        }
    }
}