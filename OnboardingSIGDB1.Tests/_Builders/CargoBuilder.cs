using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Tests._Builders;

public class CargoBuilder
{
    private long _id;
    private string _descricao;

    public CargoBuilder ComId(long id)
    {
        _id = id;
        return this;
    }

    public CargoBuilder ComDescricao(string descricao)
    {
        _descricao = descricao;
        return this;
    }

    public Cargo Build()
    {
        return new Cargo(_id, _descricao);
    }
}