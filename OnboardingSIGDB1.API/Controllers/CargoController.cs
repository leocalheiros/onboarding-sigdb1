using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Application.UseCases.Cargo;
using OnboardingSIGDB1.Application.UseCases.Cargo.DeleteCargoById;
using OnboardingSIGDB1.Application.UseCases.Cargo.FindAllCargo;
using OnboardingSIGDB1.Application.UseCases.Cargo.FindCargoById;
using OnboardingSIGDB1.Application.UseCases.Cargo.UpdateCargoById;
using OnboardingSIGDB1.Application.UseCases.Funcionario.FindFuncionarioWithFilterQuery;
using OnboardingSIGDB1.Domain.Entities.Common;

namespace OnboardingSIGDB1.API.Controllers;


[Route("api/Cargo")]
[ApiController]
public class CargoController : ControllerBase
{
    private readonly IMediator _mediator;

    public CargoController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindAllCargoQuery findAllCargoQuery, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(findAllCargoQuery, cancellationToken);

        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
    {
        var getCargo = new FindCargoByIdQuery { Id = id };
        var response = await _mediator.Send(getCargo, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCargoCommand dados, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(dados, cancellationToken);

        return Ok(response);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, [FromBody] UpdateCargoByIdCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var query = new DeleteCargoByIdQuery { Id = id };
        await _mediator.Send(query, cancellationToken);

        return NoContent();
    }
}