using System.Text.Json.Serialization;
using MediatR;

namespace OnboardingSIGDB1.Application.UseCases.Empresa.UpdateEmpresaById;

public class UpdateEmpresaByIdCommand : IRequest<UpdateEmpresaByIdResult>
{
    [JsonIgnore]
    public long Id { get; set; }
    public string Nome { get; set; }
    public string Cnpj { get; set; }
    public DateTimeOffset DataFundacao { get; set; }
}