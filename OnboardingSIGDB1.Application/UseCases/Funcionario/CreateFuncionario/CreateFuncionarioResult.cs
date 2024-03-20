namespace OnboardingSIGDB1.Application.UseCases.Funcionario.CreateFuncionario;

public class CreateFuncionarioResult
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTimeOffset DataContratacao { get; set; }
}