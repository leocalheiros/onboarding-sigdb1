using MediatR;

namespace OnboardingSIGDB1.Application.UseCases.Cargo;

public class CreateCargoCommand : IRequest<CreateCargoResult>
{
    public string Descricao { get; set; }
}