using ArcheryAcademy.Application.DTOs.ScheduleDto;
using ArcheryAcademy.Application.UseCases.ScheduleUseCase.Queries;
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

    // GET: api/schedule/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ScheduleReadDto>> GetById(Guid id)
    {
        var result = await mediator.Send(new GetScheduleByIdQuery(id));

        if (result == null)
            return NotFound($"Schedule with ID {id} not found");

        return Ok(result);
    }
}