using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Cargo.FindCargoById;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.Cargo.FindCargoById;

public class FindCargoByIdHandler : IRequestHandler<FindCargoByIdQuery, FindCargoByIdResult>
{
    private readonly IFindCargoByIdService _findCargoByIdService;
    private readonly IMapper _mapper;
    private readonly NotificationContext _notificationContext;

    public FindCargoByIdHandler(IFindCargoByIdService findCargoByIdService, IMapper mapper, NotificationContext notificationContext)
    {
        _findCargoByIdService = findCargoByIdService;
        _mapper = mapper;
        _notificationContext = notificationContext;
    }
    
    public async Task<FindCargoByIdResult> Handle(FindCargoByIdQuery request, CancellationToken cancellationToken)
    {
        var cargo = await _findCargoByIdService.ValidaCargoExistente(request.Id, cancellationToken);
        var result = new FindCargoByIdResult();
        if (_notificationContext.HasNotifications)
        {
            return result;
        }
        
        return _mapper.Map<FindCargoByIdResult>(cargo);
    }
}