namespace OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioCargoId;

public class VincularFuncionarioCargoIdResult
{
    public long FuncionarioId { get; set; }
    public long CargoId { get; set; }
    public DateTimeOffset DataVinculo { get; set; }
}