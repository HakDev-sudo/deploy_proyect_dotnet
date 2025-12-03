using ArcheryAcademy.Domain.Ports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArcheryAcademy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalendarController : ControllerBase
{
    private readonly IGoogleCalendarService _calendarService;

    public CalendarController(IGoogleCalendarService calendarService)
    {
        _calendarService = calendarService;
    }


    [HttpGet("connect/{userId:guid}")]
    [Authorize]
    public IActionResult GetAuthorizationUrl(Guid userId)
    {
        var authUrl = _calendarService.GetAuthorizationUrl(userId);
        return Ok(new { authorizationUrl = authUrl });
    }

  
    [HttpGet("callback")]
    [AllowAnonymous]
    public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state)
    {
        if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
            return BadRequest(new { message = "Código o estado inválido." });

        if (!Guid.TryParse(state, out var userId))
            return BadRequest(new { message = "Estado inválido." });

        var success = await _calendarService.ExchangeCodeForTokensAsync(code, userId);

        if (!success)
            return BadRequest(new { message = "Error al conectar con Google Calendar." });

        // Redirigir a una página de éxito o devolver mensaje
        return Ok(new { message = "Google Calendar conectado exitosamente." });
    }

   
    [HttpGet("status/{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetConnectionStatus(Guid userId)
    {
        var isConnected = await _calendarService.IsConnectedAsync(userId);
        return Ok(new { isConnected });
    }

   
    [HttpDelete("disconnect/{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> Disconnect(Guid userId)
    {
        var success = await _calendarService.DisconnectAsync(userId);

        if (!success)
            return NotFound(new { message = "No se encontró conexión con Google Calendar." });

        return Ok(new { message = "Google Calendar desconectado." });
    }

    [HttpPost("events/{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> CreateEvent(Guid userId, [FromBody] CreateEventRequest request)
    {
        var eventId = await _calendarService.CreateEventAsync(userId, new CalendarEventRequest
        {
            Title = request.Title,
            Description = request.Description,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            Location = request.Location,
            Attendees = request.Attendees
        });

        if (eventId == null)
            return BadRequest(new { message = "Error al crear evento. Verifica que Google Calendar esté conectado." });

        return CreatedAtAction(nameof(GetUpcomingEvents), new { userId }, new { eventId });
    }

  
    [HttpGet("events/{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetUpcomingEvents(Guid userId, [FromQuery] int maxResults = 10)
    {
        var events = await _calendarService.GetUpcomingEventsAsync(userId, maxResults);
        return Ok(events);
    }

 
    [HttpDelete("events/{userId:guid}/{eventId}")]
    [Authorize]
    public async Task<IActionResult> DeleteEvent(Guid userId, string eventId)
    {
        var success = await _calendarService.DeleteEventAsync(userId, eventId);

        if (!success)
            return NotFound(new { message = "Evento no encontrado o error al eliminar." });

        return NoContent();
    }
}

public class CreateEventRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string? Location { get; set; }
    public List<string>? Attendees { get; set; }
}
