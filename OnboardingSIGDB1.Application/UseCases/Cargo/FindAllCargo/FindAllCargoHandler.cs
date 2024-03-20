using MediatR;
using OnboardingSIGDB1.Application.Services.Cargo.FindAllCargo;

namespace OnboardingSIGDB1.Application.UseCases.Cargo.FindAllCargo;

public class FindAllCargoHandler : IRequestHandler<FindAllCargoQuery, List<Domain.Entities.Cargo>>
{
    private readonly IFindAllCargoService _findAllCargoService;
    
    public FindAllCargoHandler(IFindAllCargoService findAllCargoService)
    {
        _findAllCargoService = findAllCargoService;
    }
    
    public async Task<List<Domain.Entities.Cargo>> Handle(FindAllCargoQuery request, CancellationToken cancellationToken)
    {
        return await _findAllCargoService.RetornaCargosExistentes(cancellationToken);
    }
}