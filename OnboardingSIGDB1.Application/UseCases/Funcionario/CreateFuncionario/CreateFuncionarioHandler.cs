using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Empresa.CreateEmpresa;
using OnboardingSIGDB1.Application.Services.Funcionario.CreateFuncionario;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.CreateFuncionario;

public class CreateFuncionarioHandler : IRequestHandler<CreateFuncionarioCommand, CreateFuncionarioResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly IMapper _mapper;
    private readonly ICreateFuncionarioService _createFuncionarioService;
    private readonly NotificationContext _notificationContext;

    public CreateFuncionarioHandler(IUnitOfWork unitOfWork, IFuncionarioRepository funcionarioRepository,
                                    IMapper mapper,
                                    ICreateFuncionarioService createFuncionarioService,
                                    NotificationContext notificationContext
        )
    {
        _unitOfWork = unitOfWork;
        _funcionarioRepository = funcionarioRepository;
        _mapper = mapper;
        _createFuncionarioService = createFuncionarioService;
        _notificationContext = notificationContext;
    }
    
    public async Task<CreateFuncionarioResult> Handle(CreateFuncionarioCommand request, CancellationToken cancellationToken)
    {
        await _createFuncionarioService.ChecaValidacoesCpf(request, cancellationToken);
        var result = new CreateFuncionarioResult();
        if (_notificationContext.HasNotifications)
        {
            return result;
        }
        
        var funcionario = _mapper.Map<Domain.Entities.Funcionario>(request);

        await _funcionarioRepository.CreateAsync(funcionario, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<CreateFuncionarioResult>(funcionario);
    }
}