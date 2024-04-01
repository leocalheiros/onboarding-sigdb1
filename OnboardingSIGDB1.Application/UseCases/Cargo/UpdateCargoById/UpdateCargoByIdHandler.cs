using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Cargo.UpdateCargoById;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.Cargo.UpdateCargoById;

public class UpdateCargoByIdHandler : IRequestHandler<UpdateCargoByIdCommand, UpdateCargoByIdResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICargoRepository _cargoRepository;
    private readonly IMapper _mapper;
    private readonly IUpdateCargoByIdService _updateCargoByIdService;
    private readonly NotificationContext _notificationContext;

    public UpdateCargoByIdHandler(IUnitOfWork unitOfWork,
        ICargoRepository cargoRepository,
        IMapper mapper,
        IUpdateCargoByIdService updateCargoByIdService,
        NotificationContext notificationContext)
    {
        _unitOfWork = unitOfWork;
        _cargoRepository = cargoRepository;
        _mapper = mapper;
        _updateCargoByIdService = updateCargoByIdService;
        _notificationContext = notificationContext;
    }
    
    public async Task<UpdateCargoByIdResult> Handle(UpdateCargoByIdCommand request, CancellationToken cancellationToken)
    {
        var cargo = await _updateCargoByIdService.ChecaValidacoesUpdate(request.Id, request, cancellationToken);
        var result = new UpdateCargoByIdResult();
        if (_notificationContext.HasNotifications())
        {
            return result;
        }
        
        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<UpdateCargoByIdResult>(cargo);
    }
}