using AutoMapper;
using MediatR;
using OnboardingSIGDB1.Application.Services.Funcionario.FindFuncionarioByIdService;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.FindFuncionarioById;

public class FindFuncionarioByIdHandler : IRequestHandler<FindFuncionarioByIdQuery, FindFuncionarioByIdResult>
{
    private readonly IFindFuncionarioByIdService _findFuncionarioByIdService;
    private readonly IMapper _mapper;
    private readonly NotificationContext _notificationContext;

    public FindFuncionarioByIdHandler(IFindFuncionarioByIdService findFuncionarioByIdService, IMapper mapper,
        NotificationContext notificationContext)
    {
        _findFuncionarioByIdService = findFuncionarioByIdService;
        _mapper = mapper;
        _notificationContext = notificationContext;
    }
    
    public async Task<FindFuncionarioByIdResult> Handle(FindFuncionarioByIdQuery request, CancellationToken cancellationToken)
    {
        var funcionario = await _findFuncionarioByIdService.ValidaFuncionarioExistente(request.Id, cancellationToken);
        var result = new FindFuncionarioByIdResult();
        if (_notificationContext.HasNotifications())
        {
            return result;
        }

        return _mapper.Map<FindFuncionarioByIdResult>(funcionario);
    }
}