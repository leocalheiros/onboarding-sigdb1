using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Empresa.CreateEmpresa;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.Empresa.CreateEmpresa;

public class CreateEmpresaHandler : IRequestHandler<CreateEmpresaCommand, CreateEmpresaResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IMapper _mapper;
    private readonly ICreateEmpresaService _createEmpresaService;
    private readonly NotificationContext _notificationContext;

    public CreateEmpresaHandler(IUnitOfWork unitOfWork,
        IEmpresaRepository empresaRepository, 
        IMapper mapper,
        ICreateEmpresaService createEmpresaService,
        NotificationContext notificationContext)
    {
        _unitOfWork = unitOfWork;
        _empresaRepository = empresaRepository;
        _mapper = mapper;
        _createEmpresaService = createEmpresaService;
        _notificationContext = notificationContext;
    }
    
    public async Task<CreateEmpresaResult> Handle(CreateEmpresaCommand request, CancellationToken cancellationToken)
    {
        await _createEmpresaService.ChecaValidacoesCnpj(request, cancellationToken);
        var result = new CreateEmpresaResult();
        if (_notificationContext.HasNotifications())
        {
            return result;
        }
        var empresa = _mapper.Map<Domain.Entities.Empresa>(request);

        await _empresaRepository.CreateAsync(empresa, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<CreateEmpresaResult>(empresa);
    }
}