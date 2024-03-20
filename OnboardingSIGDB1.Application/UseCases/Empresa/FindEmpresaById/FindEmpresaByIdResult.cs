namespace OnboardingSIGDB1.Application.UseCases.FindEmpresaById;

public class FindEmpresaByIdResult
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public string Cnpj { get; set; }
    public DateTimeOffset DataFundacao { get; set; }
    public List<Domain.Entities.Funcionario>? Funcionarios { get; set; }
}
