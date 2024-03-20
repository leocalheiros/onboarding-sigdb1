namespace OnboardingSIGDB1.Application.Services.Funcionario._base;

public interface IBaseFuncionarioService
{
    Boolean CpfIsValid(string cpf);
    String CleanCpf(string cpf);
}