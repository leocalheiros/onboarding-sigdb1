using AutoMapper;
using OnboardingSIGDB1.Application.UseCases.FindEmpresaById;

namespace OnboardingSIGDB1.Application.UseCases.Empresa.FindEmpresaById;

public class FindEmpresaByIdMapper : Profile
{
    public FindEmpresaByIdMapper()
    {
        CreateMap<FindEmpresaByIdQuery, Domain.Entities.Empresa>();
        CreateMap<Domain.Entities.Empresa, FindEmpresaByIdResult>();
    }
}