using AutoMapper;

namespace OnboardingSIGDB1.Application.UseCases.Empresa.CreateEmpresa;

public class CreateEmpresaMapper : Profile
{
    public CreateEmpresaMapper()
    {
        CreateMap<CreateEmpresaCommand, Domain.Entities.Empresa>();
        CreateMap<Domain.Entities.Empresa, CreateEmpresaResult>();
    }
}