using ArcheryAcademy.Application.Dtos.PaymentDto;
using ArcheryAcademy.Application.UseCases.PaymentUseCase.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArcheryAcademy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController(IMediator mediator) : ControllerBase
{
    // GET: api/Payment
    [HttpGet]
    public async Task<ActionResult<List<PaymentReadDto>>> GetAll()
    {
        var result = await mediator.Send(new GetAllPaymentsQuery()); 
        
        return Ok(result);
    }
}