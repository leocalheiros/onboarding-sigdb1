using AutoMapper;

namespace OnboardingSIGDB1.Application.UseCases.Cargo.FindCargoById;

public class FindCargoByIdMapper : Profile
{
    public FindCargoByIdMapper()
    {
        CreateMap<FindCargoByIdQuery, Domain.Entities.Cargo>();
        CreateMap<Domain.Entities.Cargo, FindCargoByIdResult>();
    }    
}