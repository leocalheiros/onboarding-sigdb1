namespace OnboardingSIGDB1.Domain.Dtos;


public class FuncionarioDto
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public CargosDto Cargos { get; set; }
    public string EmpresaNome { get; set; }
    public string Cpf { get; set; }
    public DateTimeOffset DataContratacao { get; set; }
}

public class CargosDto
{
    public long Id { get; set; }
    public string CargoDescricao { get; set; }
    
}