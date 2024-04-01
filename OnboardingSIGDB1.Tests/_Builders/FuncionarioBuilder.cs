using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Tests._Builders;

public class FuncionarioBuilder
{
    private readonly Funcionario _funcionario;

    public FuncionarioBuilder()
    {
        _funcionario = new Funcionario("Nome do Funcionário", "123.456.789-00", DateTimeOffset.Now);
    }

    public FuncionarioBuilder ComId(long id)
    {
        _funcionario.AlterarIdFuncionario(id);
        return this;
    }

    public FuncionarioBuilder ComNome(string nome)
    {
        _funcionario.AlterarNomeFuncionario(nome);
        return this;
    }

    public FuncionarioBuilder ComCpf(string cpf)
    {
        _funcionario.AlterarCpfFuncionario(cpf);
        return this;
    }

    public FuncionarioBuilder ComDataContratacao(DateTimeOffset dataContratacao)
    {
        _funcionario.AlterarDataContratacao(dataContratacao);
        return this;
    }

    public FuncionarioBuilder ComEmpresaId(long? empresaId)
    {
        _funcionario.AlterarEmpresaId(empresaId ?? default(long));
        return this;
    }

    public Funcionario Build()
    {
        return _funcionario;
    }
}