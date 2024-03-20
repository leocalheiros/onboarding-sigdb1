namespace OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioEmpresaId;

public class VincularFuncionarioEmpresaIdResult
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTimeOffset DataContratacao { get; set; }
    public long EmpresaId { get; set; }
}