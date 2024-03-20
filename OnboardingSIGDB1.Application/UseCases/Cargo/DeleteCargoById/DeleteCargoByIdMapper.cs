using AutoMapper;

namespace OnboardingSIGDB1.Application.UseCases.Cargo.DeleteCargoById;

public class DeleteCargoByIdMapper : Profile
{
    public DeleteCargoByIdMapper()
    {
        CreateMap<DeleteCargoByIdQuery, Domain.Entities.Cargo>();
        CreateMap<Domain.Entities.Cargo, DeleteCargoByIdResult>();
    }
}