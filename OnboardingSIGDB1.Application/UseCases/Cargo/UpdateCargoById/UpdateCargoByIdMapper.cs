using AutoMapper;

namespace OnboardingSIGDB1.Application.UseCases.Cargo.UpdateCargoById;

public class UpdateCargoByIdMapper : Profile
{
    public UpdateCargoByIdMapper()
    {
        CreateMap<UpdateCargoByIdCommand, Domain.Entities.Cargo>();
        CreateMap<Domain.Entities.Cargo, UpdateCargoByIdResult>();
    }
}