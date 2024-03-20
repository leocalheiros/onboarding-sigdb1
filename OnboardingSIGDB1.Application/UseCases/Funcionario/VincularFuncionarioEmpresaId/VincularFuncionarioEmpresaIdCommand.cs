using MediatR;

namespace OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioEmpresaId;

public class VincularFuncionarioEmpresaIdCommand : IRequest<VincularFuncionarioEmpresaIdResult>
{
    public long FuncionarioId { get; set; }
    public long EmpresaId { get; set; }
}