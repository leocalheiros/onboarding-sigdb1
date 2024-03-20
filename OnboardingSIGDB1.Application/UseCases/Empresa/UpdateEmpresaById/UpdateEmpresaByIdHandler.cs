using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Empresa.UpdateEmpresaById;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.Empresa.UpdateEmpresaById;

public class UpdateEmpresaByIdHandler : IRequestHandler<UpdateEmpresaByIdCommand, UpdateEmpresaByIdResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IMapper _mapper;
    private readonly IUpdateEmpresaByIdService _updateEmpresaService;
    private readonly NotificationContext _notificationContext;
    
    public UpdateEmpresaByIdHandler(IUnitOfWork unitOfWork,
        IEmpresaRepository empresaRepository,
        IMapper mapper,
        IUpdateEmpresaByIdService updateEmpresaService,
        NotificationContext notificationContext)
    {
        _unitOfWork = unitOfWork;
        _empresaRepository = empresaRepository;
        _mapper = mapper;
        _updateEmpresaService = updateEmpresaService;
        _notificationContext = notificationContext;
    }
    
    public async Task<UpdateEmpresaByIdResult> Handle(UpdateEmpresaByIdCommand request, CancellationToken cancellationToken)
    {
        var empresa = await _updateEmpresaService.ChecaValidacoesUpdate(request.Id, request, cancellationToken);
        var result = new UpdateEmpresaByIdResult();
        if (_notificationContext.HasNotifications)
        {
            return result;
        }
        
        empresa.AlterarTodosDados(request.Nome, request.Cnpj, request.DataFundacao);
        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<UpdateEmpresaByIdResult>(empresa);
        
    }
}