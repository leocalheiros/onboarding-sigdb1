using AutoMapper;
using OnboardingSIGDB1.Application.UseCases.Empresa.UpdateEmpresaById;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.UpdateFuncionarioById;

public class UpdateFuncionarioByIdMapper: Profile
{
    public UpdateFuncionarioByIdMapper()
    {
        CreateMap<UpdateFuncionarioByIdCommand, Domain.Entities.Funcionario>();
        CreateMap<Domain.Entities.Funcionario, UpdateFuncionarioByIdResult>();
    }
}