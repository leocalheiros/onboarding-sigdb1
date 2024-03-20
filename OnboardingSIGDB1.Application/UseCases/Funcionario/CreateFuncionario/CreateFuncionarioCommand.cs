using MediatR;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.CreateFuncionario;

public class CreateFuncionarioCommand : IRequest<CreateFuncionarioResult>
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTimeOffset DataContratacao { get; set; }
}