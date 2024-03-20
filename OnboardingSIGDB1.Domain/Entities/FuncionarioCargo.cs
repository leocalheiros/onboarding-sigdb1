using OnboardingSIGDB1.Domain.Entities.Common;

namespace OnboardingSIGDB1.Domain.Entities;

public class FuncionarioCargo
{
    public FuncionarioCargo(long funcionarioId, long cargoId, DateTimeOffset dataVinculo)
    {
        FuncionarioId = funcionarioId;
        CargoId = cargoId;
        DataVinculo = dataVinculo;
    }
    
    public long FuncionarioId { get; private set; }
    public virtual Funcionario Funcionario { get; private set; }
    
    public long CargoId { get; private set; }
    public virtual Cargo Cargo { get; private set; }
    public DateTimeOffset DataVinculo { get; private set; }
}