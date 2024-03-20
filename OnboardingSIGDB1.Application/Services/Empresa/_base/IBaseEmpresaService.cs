namespace OnboardingSIGDB1.Application.UseCases.Empresa._base;

public interface IBaseEmpresaService
{
    Boolean CnpjIsValid(string cnpj);
    String CleanCnpj(string cnpj);
}