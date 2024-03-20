namespace OnboardingSIGDB1.Domain.Entities.Common;

public class FuncionarioFiltro
{
    public string? Nome { get; set; }
    public string? Cpf { get; set; }
    public DateTimeOffset? DataContratacaoInicio { get; set; }
    public DateTimeOffset? DataContratacaoFinal { get; set; }
}