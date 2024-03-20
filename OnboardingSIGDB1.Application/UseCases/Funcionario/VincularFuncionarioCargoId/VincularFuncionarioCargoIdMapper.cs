using AutoMapper;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioCargoId;

public class VincularFuncionarioCargoIdMapper : Profile
{
    public VincularFuncionarioCargoIdMapper()
    {
        CreateMap<VincularFuncionarioCargoIdCommand, FuncionarioCargo>();
        CreateMap<FuncionarioCargo, VincularFuncionarioCargoIdResult>();
    }
}