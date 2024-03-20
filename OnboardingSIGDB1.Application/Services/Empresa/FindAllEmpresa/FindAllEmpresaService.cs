using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Application.Services.Empresa.FindAllEmpresa;

public class FindAllEmpresaService : IFindAllEmpresaService
{
    private readonly IEmpresaRepository _empresaRepository;
    
    public FindAllEmpresaService(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }
    
    public async Task<List<Domain.Entities.Empresa>> RetornaEmpresasExistentes(CancellationToken cancellationToken)
    {
        var empresasExistentes = await _empresaRepository.GetAllAsync(cancellationToken);
        if (empresasExistentes == null)
        {
            throw new Exception("Empresas não encontradas!");
        }

        return empresasExistentes;
    }
}