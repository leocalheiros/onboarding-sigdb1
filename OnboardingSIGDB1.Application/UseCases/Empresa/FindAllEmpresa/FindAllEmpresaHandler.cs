using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Empresa.FindAllEmpresa;

namespace OnboardingSIGDB1.Application.UseCases.Empresa.FindAllEmpresa;

public class FindAllEmpresaHandler : IRequestHandler<FindAllEmpresaQuery, List<Domain.Entities.Empresa>>
{
    private readonly IFindAllEmpresaService _empresaService;
    private readonly IMapper _mapper;
    
    public FindAllEmpresaHandler(IFindAllEmpresaService empresaService, IMapper mapper)
    {
        _empresaService = empresaService;
    }


    public async Task<List<Domain.Entities.Empresa>> Handle(FindAllEmpresaQuery request, CancellationToken cancellationToken)
    {
        return await _empresaService.RetornaEmpresasExistentes(cancellationToken);
    }
}