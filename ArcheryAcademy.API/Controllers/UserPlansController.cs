using ArcheryAcademy.Application.DTOs.UserPlanDto;
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
}