using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Funcionario.VincularFuncionarioEmpresaIdService;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioEmpresaId;

public class VincularFuncionarioEmpresaIdHandler : IRequestHandler<VincularFuncionarioEmpresaIdCommand, VincularFuncionarioEmpresaIdResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IVincularFuncionarioEmpresaIdService _service;
    private readonly NotificationContext _notificationContext;

    public VincularFuncionarioEmpresaIdHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IVincularFuncionarioEmpresaIdService service,
        NotificationContext notificationContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _notificationContext = notificationContext;
    }
    
    public async Task<VincularFuncionarioEmpresaIdResult> Handle(VincularFuncionarioEmpresaIdCommand request, CancellationToken cancellationToken)
    {
        var funcionario = await _service.VincularFuncionarioEmpresaAsync(request, cancellationToken);
        var result = new VincularFuncionarioEmpresaIdResult();

        if (_notificationContext.HasNotifications())
        {
            return result;
        }
        await _unitOfWork.Commit(cancellationToken);
        return _mapper.Map<VincularFuncionarioEmpresaIdResult>(funcionario);
        
    }
}