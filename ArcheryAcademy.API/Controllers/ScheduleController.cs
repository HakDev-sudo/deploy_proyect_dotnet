using ArcheryAcademy.Application.DTOs.ScheduleDto;
using ArcheryAcademy.Application.UseCases.ScheduleUseCase.Commands;
using ArcheryAcademy.Application.UseCases.ScheduleUseCase.Queries;
using ArcheryAcademy.Infrastructure.Persistence.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArcheryAcademy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController(IMediator mediator) : ControllerBase
{
    // GET: api/schedule
    [HttpGet]
    public async Task<ActionResult<List<ScheduleReadDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllSchedulesQuery());
        return Ok(result);
    }

    // GET by id
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetScheduleByIdQuery(id));
        if (result == null)
            return NotFound(new { message = $"Schedule with ID {id} not found." });

        return Ok(result);
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> CreateSchedule([FromBody] ScheduleCreateDto dto)
    {
        var command = new CreateScheduleCommand(dto);
        var result = await mediator.Send(command);
        return Ok(result);
    }
}