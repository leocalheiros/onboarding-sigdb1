using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Application.UseCases.Empresa.CreateEmpresa;
using OnboardingSIGDB1.Application.UseCases.Empresa.DeleteEmpresaById;
using OnboardingSIGDB1.Application.UseCases.Empresa.FindAllEmpresa;
using OnboardingSIGDB1.Application.UseCases.Empresa.UpdateEmpresaById;
using OnboardingSIGDB1.Application.UseCases.FindEmpresaById;
using OnboardingSIGDB1.Application.UseCases.FindEmpresaWithFilterQuery;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Entities.Common;

namespace OnboardingSIGDB1.API.Controllers;


[Route("api/Empresa")]
[ApiController]
public class EmpresaController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmpresaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindAllEmpresaQuery findAllEmpresaQuery, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(findAllEmpresaQuery, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
    {
        var getEmpresa = new FindEmpresaByIdQuery { Id = id };
        var response = await _mediator.Send(getEmpresa, cancellationToken);
        return Ok(response);
    }

    [HttpGet("pesquisar")]
    public async Task<IActionResult> Get([FromQuery] EmpresaFiltro filtro, CancellationToken cancellationToken)
    {
        var query = new FindEmpresasWithFiltersQuery { Filtro = filtro };
        var empresas = await _mediator.Send(query, cancellationToken);
        return Ok(empresas);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateEmpresaCommand dados, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(dados, cancellationToken);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, [FromBody] UpdateEmpresaByIdCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var query = new DeleteEmpresaByIdQuery { Id = id };
        await _mediator.Send(query, cancellationToken);

        return NoContent();
    }
}