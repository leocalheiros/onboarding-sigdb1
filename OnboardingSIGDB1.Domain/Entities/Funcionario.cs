using OnboardingSIGDB1.Domain.Entities.Common;

namespace OnboardingSIGDB1.Domain.Entities;

public class Funcionario : BaseEntity
{

    public Funcionario(string nome, string cpf, DateTimeOffset dataContratacao)
    {
        Nome = nome;
        Cpf = cpf;
        DataContratacao = dataContratacao;
    }
    
    public long Id { get; private set; }
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public DateTimeOffset DataContratacao { get; private set; }
    public long? EmpresaId { get; private set; }
    public virtual Empresa? Empresa { get; private set; }
    public virtual List<FuncionarioCargo>? CargosFuncionario { get; private set; } = new List<FuncionarioCargo>();

    public void AlterarTodosDadosFuncionario(string nome, string cpf, DateTimeOffset dataContratacao)
    {
        Nome = nome;
        Cpf = cpf;
        DataContratacao = dataContratacao;
    }

    public void AlterarIdFuncionario(long id)
    {
        Id = id;
    }
    
    public void AlterarNomeFuncionario(string nome)
    {
        Nome = nome;
    }

    public void AlterarCpfFuncionario(string cpf)
    {
        Cpf = cpf;
    }

    public void AlterarDataContratacao(DateTimeOffset dataContratacao)
    {
        DataContratacao = dataContratacao;
    }

    public void AlterarEmpresaId(long empresaId)
    {
        EmpresaId = empresaId;
    }
    
    public void AdicionarCargo(FuncionarioCargo funcionarioCargo)
    {
        if (CargosFuncionario == null)
        {
            CargosFuncionario = new List<FuncionarioCargo>();
        }
        CargosFuncionario.Add(funcionarioCargo);
    }
}