namespace ArcheryAcademy.Domain.Ports.Services;

public interface IGoogleCalendarService
{
    
    string GetAuthorizationUrl(Guid userId);

    
    Task<bool> ExchangeCodeForTokensAsync(string code, Guid userId);

   
    Task<string?> CreateEventAsync(Guid userId, CalendarEventRequest eventRequest);

    Task<IEnumerable<CalendarEventResponse>> GetUpcomingEventsAsync(Guid userId, int maxResults = 10);

    
    Task<bool> DeleteEventAsync(Guid userId, string eventId);

    Task<bool> IsConnectedAsync(Guid userId);

    Task<bool> DisconnectAsync(Guid userId);
}

public class CalendarEventRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string? Location { get; set; }
    public List<string>? Attendees { get; set; }
}

public class CalendarEventResponse
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string? Location { get; set; }
    public string? HtmlLink { get; set; }
}
