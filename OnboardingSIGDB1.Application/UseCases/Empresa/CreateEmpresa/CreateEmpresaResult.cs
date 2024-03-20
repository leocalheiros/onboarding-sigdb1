namespace OnboardingSIGDB1.Application.UseCases.Empresa.CreateEmpresa;

public class CreateEmpresaResult
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public string Cnpj { get; set; }
    public DateTimeOffset DataFundacao { get; set; }
}