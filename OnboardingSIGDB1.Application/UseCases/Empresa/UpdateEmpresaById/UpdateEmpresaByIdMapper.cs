using AutoMapper;

namespace OnboardingSIGDB1.Application.UseCases.Empresa.UpdateEmpresaById;

public class UpdateEmpresaByIdMapper : Profile
{
    public UpdateEmpresaByIdMapper()
    {
        CreateMap<UpdateEmpresaByIdCommand, Domain.Entities.Empresa>();
        CreateMap<Domain.Entities.Empresa, UpdateEmpresaByIdResult>();
    }
}