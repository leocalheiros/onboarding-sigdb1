using AutoMapper;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioEmpresaId;

public class VincularFuncionarioEmpresaIdMapper : Profile
{
    public VincularFuncionarioEmpresaIdMapper()
    {
        CreateMap<VincularFuncionarioEmpresaIdCommand, Domain.Entities.Funcionario>();
        CreateMap<Domain.Entities.Funcionario, VincularFuncionarioEmpresaIdResult>();
    }
    
}