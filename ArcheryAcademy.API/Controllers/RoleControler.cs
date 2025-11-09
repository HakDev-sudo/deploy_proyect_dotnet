using ArcheryAcademy.Application.DTOs.RolDto;
using ArcheryAcademy.Application.UseCases.PlanUseCases.Queries;
using ArcheryAcademy.Application.UseCases.RoleUseCases.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArcheryAcademy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleControler(IMediator mediator, IMapper mapper): ControllerBase
{
    // GET all
    [HttpGet]
    public async Task<ActionResult<List<RolReadDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllRoleQuery());
        return Ok(result);
    }
    
    // GET by id
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetRoleByIdQuery(id));
        
        if (result == null)
            return NotFound(new { message = $"Payment with ID {id} not found." });

        return Ok(result);
    }
}