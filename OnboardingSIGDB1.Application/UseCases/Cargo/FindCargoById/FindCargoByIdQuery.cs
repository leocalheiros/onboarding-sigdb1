using MediatR;

namespace OnboardingSIGDB1.Application.UseCases.Cargo.FindCargoById;

public class FindCargoByIdQuery : IRequest<FindCargoByIdResult>
{
    public long Id { get; set; }
}