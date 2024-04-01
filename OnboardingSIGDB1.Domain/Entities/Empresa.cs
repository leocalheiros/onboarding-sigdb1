using OnboardingSIGDB1.Domain.Entities.Common;

namespace OnboardingSIGDB1.Domain.Entities;

public class Empresa : BaseEntity
{

    public Empresa(string nome, string cnpj, DateTimeOffset dataFundacao)
    {
        Nome = nome;
        Cnpj = cnpj;
        DataFundacao = dataFundacao;
    }
    
    public long Id { get; private set; }
    public string Nome { get; private set; }
    public string Cnpj { get; private set; }
    public DateTimeOffset DataFundacao { get; private set; }
    public virtual ICollection<Funcionario> Funcionarios { get; private set; }
    
    public void AlterarNomeEmpresa(string nome)
    {
        Nome = nome;
    }

    public void AlterarCnpj(string cnpj)
    {
        Cnpj = cnpj;
    }

    public void AlterarDataFundacao(DateTimeOffset dataFundacao)
    {
        DataFundacao = dataFundacao;
    }

    public void AlterarTodosDados(string nome, string cnpj, DateTimeOffset dataFundacao)
    {
        Nome = nome;
        Cnpj = cnpj;
        DataFundacao = dataFundacao;
    }
}