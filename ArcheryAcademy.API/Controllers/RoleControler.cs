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
}