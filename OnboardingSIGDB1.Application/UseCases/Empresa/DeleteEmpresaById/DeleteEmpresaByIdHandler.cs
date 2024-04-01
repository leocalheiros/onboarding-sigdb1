using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Empresa.DeleteEmpresaById;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.Empresa.DeleteEmpresaById;

public class DeleteEmpresaByIdHandler : IRequestHandler<DeleteEmpresaByIdQuery, DeleteEmpresaByIdResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IMapper _mapper;
    private readonly IDeleteEmpresaByIdService _deleteEmpresaByIdService;
    private readonly NotificationContext _notificationContext;
    
    public DeleteEmpresaByIdHandler(IUnitOfWork unitOfWork,
        IEmpresaRepository empresaRepository,
        IMapper mapper,
        IDeleteEmpresaByIdService deleteEmpresaByIdService,
        NotificationContext notificationContext)
    {
        _unitOfWork = unitOfWork;
        _empresaRepository = empresaRepository;
        _mapper = mapper;
        _deleteEmpresaByIdService = deleteEmpresaByIdService;
        _notificationContext = notificationContext;
    }

    public async Task<DeleteEmpresaByIdResult> Handle(DeleteEmpresaByIdQuery request, CancellationToken cancellationToken)
    {
        var empresa = await _deleteEmpresaByIdService.ChecaValidacoesDeleteEmpresa(request.Id, cancellationToken);
        var result = new DeleteEmpresaByIdResult();
        if (_notificationContext.HasNotifications())
        {
            return result;
        }
        
        _empresaRepository.Delete(empresa);
        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<DeleteEmpresaByIdResult>(empresa);
    }
}