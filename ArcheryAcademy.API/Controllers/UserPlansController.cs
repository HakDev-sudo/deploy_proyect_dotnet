using ArcheryAcademy.Application.DTOs.UserPlanDto;
using ArcheryAcademy.Application.UseCases.UserPlanUseCases.Commands;
using ArcheryAcademy.Application.UseCases.UserPlanUseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArcheryAcademy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserPlansController(IMediator mediator) : ControllerBase
{
    // GET: api/userplans
    [HttpGet]
    public async Task<ActionResult<List<UserPlanReadDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllUserPlansQuery());
        return Ok(result);
    }
    
    // GET by id
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetUserPlanByIdQuery(id));
        if (result == null)
            return NotFound(new { message = $"UserPlan with ID {id} not found." });

        return Ok(result);
    }
    
    // post 
    [HttpPost]
    public async Task<IActionResult> CreateUserPlan([FromBody] UserPlanCreateDto dto)
    {
        var command = new CreateUserPlanCommand(dto);
        var result = await mediator.Send(command);
        return Ok(result);
    }
}