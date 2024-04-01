using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Cargo.DeleteCargoById;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.Cargo.DeleteCargoById;

public class DeleteCargoByIdHandler : IRequestHandler<DeleteCargoByIdQuery, DeleteCargoByIdResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICargoRepository _cargoRepository;
    private readonly IMapper _mapper;
    private readonly IDeleteCargoByIdService _deleteCargoByIdService;
    private readonly NotificationContext _notificationContext;

    public DeleteCargoByIdHandler(IUnitOfWork unitOfWork,
        ICargoRepository cargoRepository,
        IMapper mapper,
        IDeleteCargoByIdService deleteCargoByIdService,
        NotificationContext notificationContext)
    {
        _unitOfWork = unitOfWork;
        _cargoRepository = cargoRepository;
        _mapper = mapper;
        _deleteCargoByIdService = deleteCargoByIdService;
        _notificationContext = notificationContext;
    }
    
    public async Task<DeleteCargoByIdResult> Handle(DeleteCargoByIdQuery request, CancellationToken cancellationToken)
    {
        var cargo = await _deleteCargoByIdService.ChecaValidacoesDeleteCargo(request.Id, cancellationToken);
        var result = new DeleteCargoByIdResult();
        if (_notificationContext.HasNotifications())
        {
            return result;
        }
        _cargoRepository.Delete(cargo);
        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<DeleteCargoByIdResult>(cargo);
    }
}