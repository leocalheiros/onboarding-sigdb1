using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Funcionario.VincularFuncionarioCargoid;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioCargoId;

public class VincularFuncionarioCargoIdHandler : IRequestHandler<VincularFuncionarioCargoIdCommand, VincularFuncionarioCargoIdResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IVincularFuncionarioCargoIdService _service;
    private readonly NotificationContext _notificationContext;

    public VincularFuncionarioCargoIdHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IVincularFuncionarioCargoIdService service,
        NotificationContext notificationContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _notificationContext = notificationContext;
    }
    
    public async Task<VincularFuncionarioCargoIdResult> Handle(VincularFuncionarioCargoIdCommand request, CancellationToken cancellationToken)
    {
        var funcionarioCargo = await _service.VincularFuncionarioCargoIdAsync(request, cancellationToken);
        var result = new VincularFuncionarioCargoIdResult();

        if (_notificationContext.HasNotifications)
        {
            return result;
        }
        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<VincularFuncionarioCargoIdResult>(funcionarioCargo);
    }
}