using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Application.Data;
using OnboardingSIGDB1.Application.Services.Behaviors;
using OnboardingSIGDB1.Application.Services.Cargo.CreateCargo;
using OnboardingSIGDB1.Application.Services.Cargo.DeleteCargoById;
using OnboardingSIGDB1.Application.Services.Cargo.FindAllCargo;
using OnboardingSIGDB1.Application.Services.Cargo.FindCargoById;
using OnboardingSIGDB1.Application.Services.Cargo.UpdateCargoById;
using OnboardingSIGDB1.Application.Services.Empresa.CreateEmpresa;
using OnboardingSIGDB1.Application.Services.Empresa.DeleteEmpresaById;
using OnboardingSIGDB1.Application.Services.Empresa.FindAllEmpresa;
using OnboardingSIGDB1.Application.Services.Empresa.FindEmpresaById;
using OnboardingSIGDB1.Application.Services.Empresa.FindEmpresaWithFilters;
using OnboardingSIGDB1.Application.Services.Empresa.UpdateEmpresaById;
using OnboardingSIGDB1.Application.Services.Funcionario.CreateFuncionario;
using OnboardingSIGDB1.Application.Services.Funcionario.DeleteFuncionarioByIdService;
using OnboardingSIGDB1.Application.Services.Funcionario.FindAllFuncionarioService;
using OnboardingSIGDB1.Application.Services.Funcionario.FindFuncionarioByIdService;
using OnboardingSIGDB1.Application.Services.Funcionario.FindFuncionarioWithFiltersService;
using OnboardingSIGDB1.Application.Services.Funcionario.UpdateFuncionarioByIdService;
using OnboardingSIGDB1.Application.Services.Funcionario.VincularFuncionarioCargoid;
using OnboardingSIGDB1.Application.Services.Funcionario.VincularFuncionarioEmpresaIdService;
using OnboardingSIGDB1.Application.UseCases.Empresa.CreateEmpresa;
using OnboardingSIGDB1.Data.Persistence;
using OnboardingSIGDB1.Data.Repositories;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Data;

public static class DepencendyInjectionExtensions
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OnboardingSIGContext>(options =>
            options.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=OnboardingSIGDB1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        );

        services.AddScoped<IDatabaseContext, OnboardingSIGContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
        services.AddScoped<ICargoRepository, CargoRepository>();
        services.AddScoped<IFuncionarioCargoRepository, FuncionarioCargoRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateEmpresaService, CreateEmpresaService>();
        services.AddScoped<IFindAllEmpresaService, FindAllEmpresaService>();
        services.AddScoped<IFindEmpresaByIdService, FindEmpresaByIdService>();
        services.AddScoped<IUpdateEmpresaByIdService, UpdateEmpresaByIdService>();
        services.AddScoped<IFindEmpresaWithFiltersService, FindEmpresaWithFiltersService>();
        services.AddScoped<ICreateFuncionarioService, CreateFuncionarioService>();
        services.AddScoped<IFindAllFuncionarioService, FindAllFuncionarioService>();
        services.AddScoped<IFindFuncionarioByIdService, FindFuncionarioByIdService>();
        services.AddScoped<IUpdateFuncionarioByIdService, UpdateFuncionarioByIdService>();
        services.AddScoped<IDeleteEmpresaByIdService, DeleteEmpresaByIdService>();
        services.AddScoped<IDeleteFuncionarioByIdService, DeleteFuncionarioByIdService>();
        services.AddScoped<ICreateCargoService, CreateCargoService>();
        services.AddScoped<IFindAllCargoService, FindAllCargoService>();
        services.AddScoped<IFindCargoByIdService, FindCargoByIdService>();
        services.AddScoped<IUpdateCargoByIdService, UpdateCargoByIdService>();
        services.AddScoped<IDeleteCargoByIdService, DeleteCargoByIdService>();
        services.AddScoped<IFindFuncionarioWithFiltersService, FindFuncionarioWithFiltersService>();
        services.AddScoped<IVincularFuncionarioEmpresaIdService, VincularFuncionarioEmpresaIdService>();
        services.AddScoped<IVincularFuncionarioCargoIdService, VincularFuncionarioCargoIdService>();
        
        services.AddScoped<NotificationContext>();
        
        services.AddMvc(options => options.Filters.Add<NotificationFilter>())
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
    }

    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ValidationBehavior<,>).GetTypeInfo().Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    public static void AddAutoMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CreateEmpresaMapper));
    }
    
    public static void AddValidators(this IServiceCollection services) =>
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    
}