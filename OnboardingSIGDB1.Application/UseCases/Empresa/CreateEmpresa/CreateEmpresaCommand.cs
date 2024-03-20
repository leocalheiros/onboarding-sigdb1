using MediatR;

namespace OnboardingSIGDB1.Application.UseCases.Empresa.CreateEmpresa;

public class CreateEmpresaCommand : IRequest<CreateEmpresaResult>
{
    public string Nome { get; set; }
    public string Cnpj { get; set; }
    public DateTimeOffset DataFundacao { get; set; }
}