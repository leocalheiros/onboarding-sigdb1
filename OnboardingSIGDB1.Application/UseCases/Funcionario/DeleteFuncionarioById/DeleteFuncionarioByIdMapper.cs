using AutoMapper;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.DeleteFuncionarioById;

public class DeleteFuncionarioByIdMapper : Profile
{
    public DeleteFuncionarioByIdMapper()
    {
        CreateMap<DeleteFuncionarioByIdQuery, Domain.Entities.Funcionario>();
        CreateMap<Domain.Entities.Funcionario, DeleteFuncionarioByIdResult>();
    }
}