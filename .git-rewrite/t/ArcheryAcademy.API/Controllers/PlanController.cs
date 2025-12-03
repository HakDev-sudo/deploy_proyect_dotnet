using ArcheryAcademy.Application.Dtos.PlanDto;
using ArcheryAcademy.Application.UseCases.PlanUseCases.Commands;
using ArcheryAcademy.Application.UseCases.PlanUseCases.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArcheryAcademy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanController(IMediator mediator, IMapper mapper) : ControllerBase
{
    // GET: api/plan
    [HttpGet]
    public async Task<ActionResult<List<PlanReadDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllPlansQuery());
        return Ok(result);
    }

    // GET by id
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetPlanByIdQuery(id));
        
        if (result == null)
            return NotFound(new { message = $"Plan with ID {id} not found." });

        return Ok(result);
    }

    // POST (Crear)
    [HttpPost]
    public async Task<IActionResult> CreatePlan([FromBody] PlanCreateDto dto)
    {
        var command = new CreatePlanCommand(dto);
        var createdEntity = await mediator.Send(command); 
        
        // Mapear la entidad (Plan) al DTO (PlanReadDto)
        var resultDto = mapper.Map<PlanReadDto>(createdEntity);
        
        return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
    }

    // PUT (Actualizar)
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePlan(Guid id, [FromBody] PlanUpdateDto dto)
    {
        var command = new UpdatePlanCommand(id, dto);
        var updatedEntity = await mediator.Send(command); 

        if (updatedEntity is null)
            return NotFound($"Plan with ID {id} not found.");
        
        // Mapear la entidad actualizada al DTO de lectura
        var resultDto = mapper.Map<PlanReadDto>(updatedEntity);

        return Ok(resultDto);
    }
    
    // DELETE
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeletePlanCommand(id));

        if (!result)
            return NotFound($"Plan with ID {id} was not found.");

        return NoContent(); 
    }
}