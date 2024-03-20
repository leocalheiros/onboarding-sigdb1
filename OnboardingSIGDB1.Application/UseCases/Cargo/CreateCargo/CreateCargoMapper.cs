using AutoMapper;

namespace OnboardingSIGDB1.Application.UseCases.Cargo;

public class CreateCargoMapper : Profile
{
    public CreateCargoMapper()
    {
        CreateMap<CreateCargoCommand, Domain.Entities.Cargo>();
        CreateMap<Domain.Entities.Cargo, CreateCargoResult>();
    }
}