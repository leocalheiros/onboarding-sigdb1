using AutoMapper;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.CreateFuncionario;

public class CreateFuncionarioMapper : Profile
{
    public CreateFuncionarioMapper()
    {
        CreateMap<CreateFuncionarioCommand, Domain.Entities.Funcionario>();
        CreateMap<Domain.Entities.Funcionario, CreateFuncionarioResult>();
    }
}