using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Empresa.FindEmpresaById;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.FindEmpresaById;

public class FindEmpresaByIdHandler : IRequestHandler<FindEmpresaByIdQuery, FindEmpresaByIdResult>
{
    private readonly IFindEmpresaByIdService _findEmpresaByIdService;
    private readonly IMapper _mapper;
    private readonly NotificationContext _notificationContext;
    
    public FindEmpresaByIdHandler(IFindEmpresaByIdService findEmpresaByIdService, IMapper mapper, NotificationContext notificationContext)
    {
        _findEmpresaByIdService = findEmpresaByIdService;
        _mapper = mapper;
        _notificationContext = notificationContext;
    }
    
    public async Task<FindEmpresaByIdResult> Handle(FindEmpresaByIdQuery request, CancellationToken cancellationToken)
    {
        var empresa = await _findEmpresaByIdService.ValidaEmpresaExistente(request.Id, cancellationToken);
        var result = new FindEmpresaByIdResult();
        if (_notificationContext.HasNotifications())
        {
            return result;
        }
        
        return _mapper.Map<FindEmpresaByIdResult>(empresa);

    }
}