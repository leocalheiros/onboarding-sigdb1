using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Empresa.UpdateEmpresaById;
using OnboardingSIGDB1.Application.Services.Funcionario.UpdateFuncionarioByIdService;
using OnboardingSIGDB1.Application.UseCases.Empresa.UpdateEmpresaById;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.UpdateFuncionarioById;

public class UpdateFuncionarioByIdHandler : IRequestHandler<UpdateFuncionarioByIdCommand, UpdateFuncionarioByIdResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly IMapper _mapper;
    private readonly IUpdateFuncionarioByIdService _updateFuncionarioByIdService;
    private readonly NotificationContext _notificationContext;

    public UpdateFuncionarioByIdHandler(IUnitOfWork unitOfWork,
        IFuncionarioRepository funcionarioRepository,
        IMapper mapper,
        IUpdateFuncionarioByIdService updateFuncionarioByIdService,
        NotificationContext notificationContext)
    {
        _unitOfWork = unitOfWork;
        _funcionarioRepository = funcionarioRepository;
        _mapper = mapper;
        _updateFuncionarioByIdService = updateFuncionarioByIdService;
        _notificationContext = notificationContext;
    }

    public async Task<UpdateFuncionarioByIdResult> Handle(UpdateFuncionarioByIdCommand request, CancellationToken cancellationToken)
    {
        var funcionario =
            await _updateFuncionarioByIdService.ChecaValidacoesUpdate(request.Id, request, cancellationToken);
        var result = new UpdateFuncionarioByIdResult();
        if (_notificationContext.HasNotifications())
        {
            return result;
        }
        
        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<UpdateFuncionarioByIdResult>(funcionario);

    }
}