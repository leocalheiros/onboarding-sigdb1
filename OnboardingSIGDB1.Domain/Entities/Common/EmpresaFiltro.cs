namespace OnboardingSIGDB1.Domain.Entities.Common;

public class EmpresaFiltro
{
    public string? Nome { get; set; }
    public string? Cnpj { get; set; }
    public DateTimeOffset? DataFundacaoInicio { get; set; }
    public DateTimeOffset? DataFundacaoFim { get; set; }
}