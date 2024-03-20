using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.FindFuncionarioById;

public class FindFuncionarioByIdResult
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public long EmpresaId { get; set; }
    public DateTimeOffset DataContratacao { get; set; }
}