using AutoMapper;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.FindFuncionarioById;

public class FindFuncionarioByIdMapper : Profile
{
    public FindFuncionarioByIdMapper()
    {
        CreateMap<FindFuncionarioByIdQuery, Domain.Entities.Funcionario>();
        CreateMap<Domain.Entities.Funcionario, FindFuncionarioByIdResult>();
    }
}