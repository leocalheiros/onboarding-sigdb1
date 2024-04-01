using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Cargo.CreateCargo;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.Cargo.CreateCargo;

public class CreateCargoHandler : IRequestHandler<CreateCargoCommand, CreateCargoResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICargoRepository _cargoRepository;
    private readonly IMapper _mapper;
    private readonly ICreateCargoService _createCargoService;
    private readonly NotificationContext _notificationContext;
    
    public CreateCargoHandler(IUnitOfWork unitOfWork,
        ICargoRepository cargoRepository,
        IMapper mapper,
        ICreateCargoService createCargoService,
        NotificationContext notificationContext)
    {
        _unitOfWork = unitOfWork;
        _cargoRepository = cargoRepository;
        _mapper = mapper;
        _createCargoService = createCargoService;
        _notificationContext = notificationContext;
    }
    
    public async Task<CreateCargoResult> Handle(CreateCargoCommand request, CancellationToken cancellationToken)
    {
        await _createCargoService.ChecaValidacoesCargo(request, cancellationToken);
        var result = new CreateCargoResult();
        if (_notificationContext.HasNotifications())
        {
            return result;
        }
        var cargo = _mapper.Map<Domain.Entities.Cargo>(request);

        await _cargoRepository.CreateAsync(cargo, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return  _mapper.Map<CreateCargoResult>(cargo);
    }
}