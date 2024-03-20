using System.Text.Json.Serialization;
using MediatR;

namespace OnboardingSIGDB1.Application.UseCases.Cargo.UpdateCargoById;

public class UpdateCargoByIdCommand : IRequest<UpdateCargoByIdResult>
{
    [JsonIgnore]
    public long Id { get; set; }
    public string Descricao { get; set; }
}