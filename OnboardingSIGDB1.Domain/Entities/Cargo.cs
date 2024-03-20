using OnboardingSIGDB1.Domain.Entities.Common;

namespace OnboardingSIGDB1.Domain.Entities;

public class Cargo : BaseEntity
{
    public Cargo(long id, string descricao)
    {
        Id = id;
        Descricao = descricao;
    }
    
    public Cargo(string descricao)
    {
        Descricao = descricao;
    }
    
    public long Id { get; private set; }
    public string Descricao { get; private set; }

    public void AlteraDescricao(string descricao)
    {
        Descricao = descricao;
    }
}