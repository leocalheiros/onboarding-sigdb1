using AutoMapper;

namespace OnboardingSIGDB1.Application.UseCases.Empresa.DeleteEmpresaById;

public class DeleteEmpresaByIdMapper : Profile
{
    public DeleteEmpresaByIdMapper()
    {
        CreateMap<DeleteEmpresaByIdQuery, Domain.Entities.Empresa>();
        CreateMap<Domain.Entities.Empresa, DeleteEmpresaByIdResult>();
    }
}