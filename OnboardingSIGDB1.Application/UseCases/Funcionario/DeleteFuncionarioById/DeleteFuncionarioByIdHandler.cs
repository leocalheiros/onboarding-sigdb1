using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Empresa.DeleteEmpresaById;
using OnboardingSIGDB1.Application.Services.Funcionario.DeleteFuncionarioByIdService;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.DeleteFuncionarioById;

public class DeleteFuncionarioByIdHandler : IRequestHandler<DeleteFuncionarioByIdQuery, DeleteFuncionarioByIdResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly IMapper _mapper;
    private readonly IDeleteFuncionarioByIdService _deleteFuncionarioByIdService;
    private readonly NotificationContext _notificationContext;

    public DeleteFuncionarioByIdHandler(IUnitOfWork unitOfWork,
        IFuncionarioRepository funcionarioRepository,
        IMapper mapper,
        IDeleteFuncionarioByIdService deleteFuncionarioByIdService,
        NotificationContext notificationContext)
    {
        _unitOfWork = unitOfWork;
        _funcionarioRepository = funcionarioRepository;
        _mapper = mapper;
        _deleteFuncionarioByIdService = deleteFuncionarioByIdService;
        _notificationContext = notificationContext;
    }

    public async Task<DeleteFuncionarioByIdResult> Handle(DeleteFuncionarioByIdQuery request, CancellationToken cancellationToken)
    {
        var funcionario = await _deleteFuncionarioByIdService.
            ChecaFuncionarioExistente(request.Id, cancellationToken);
        var result = new DeleteFuncionarioByIdResult();
        if (_notificationContext.HasNotifications)
        {
            return result;
        }
        
        _funcionarioRepository.Delete(funcionario);
        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<DeleteFuncionarioByIdResult>(funcionario);
        
    }
}