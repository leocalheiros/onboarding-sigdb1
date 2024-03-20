using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Application.UseCases.Funcionario.CreateFuncionario;
using OnboardingSIGDB1.Application.UseCases.Funcionario.DeleteFuncionarioById;
using OnboardingSIGDB1.Application.UseCases.Funcionario.FindAllFuncionario;
using OnboardingSIGDB1.Application.UseCases.Funcionario.FindFuncionarioById;
using OnboardingSIGDB1.Application.UseCases.Funcionario.FindFuncionarioWithFilterQuery;
using OnboardingSIGDB1.Application.UseCases.Funcionario.UpdateFuncionarioById;
using OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioCargoId;
using OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioEmpresaId;
using OnboardingSIGDB1.Domain.Entities.Common;

namespace OnboardingSIGDB1.API.Controllers;

[Route("api/Funcionario")]
[ApiController]
public class FuncionarioController : ControllerBase
{
    private readonly IMediator _mediator;

    public FuncionarioController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindAllFuncionarioQuery findAllFuncionarioQuery, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(findAllFuncionarioQuery, cancellationToken);

        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
    {
        var getFuncionario = new FindFuncionarioByIdQuery { Id = id };
        var response = await _mediator.Send(getFuncionario, cancellationToken);
        return Ok(response);
    }
    
    [HttpGet("pesquisar")]
    public async Task<IActionResult> Get([FromQuery] FuncionarioFiltro filtro, CancellationToken cancellationToken)
    {
        var query = new FindFuncionarioWithFiltersQuery { Filtro = filtro };
        var funcionarios = await _mediator.Send(query, cancellationToken);
        return Ok(funcionarios);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateFuncionarioCommand dados, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(dados, cancellationToken);

        return Ok(response);
    }

    [HttpPost("vincular-empresa")]
    public async Task<IActionResult> Post([FromBody] VincularFuncionarioEmpresaIdCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(response);
    }
    
    [HttpPost("vincular-cargo")]
    public async Task<IActionResult> Post([FromBody] VincularFuncionarioCargoIdCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(response);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, [FromBody] UpdateFuncionarioByIdCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var query = new DeleteFuncionarioByIdQuery { Id = id };
        await _mediator.Send(query, cancellationToken);

        return NoContent();
    }
}