using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports.Repositories;
using ArcheryAcademy.Domain.Ports.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;

namespace ArcheryAcademy.Infrastructure.Adapters.Services;

public class GoogleCalendarService : IGoogleCalendarService
{
    private readonly IGoogleTokenRepository _tokenRepository;
    private readonly IConfiguration _configuration;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _redirectUri;

    public GoogleCalendarService(
        IGoogleTokenRepository tokenRepository,
        IConfiguration configuration)
    {
        _tokenRepository = tokenRepository;
        _configuration = configuration;
        
        _clientId = configuration["Google:ClientId"] 
            ?? throw new InvalidOperationException("Google:ClientId no está configurado");
        _clientSecret = configuration["Google:ClientSecret"] 
            ?? throw new InvalidOperationException("Google:ClientSecret no está configurado");
        _redirectUri = configuration["Google:RedirectUri"] 
            ?? throw new InvalidOperationException("Google:RedirectUri no está configurado");
    }

    public string GetAuthorizationUrl(Guid userId)
    {
        var flow = CreateGoogleAuthorizationCodeFlow();
        
        var authUrl = flow.CreateAuthorizationCodeRequest(_redirectUri);
        authUrl.Scope = CalendarService.Scope.Calendar;
        authUrl.State = userId.ToString(); // Pasamos el userId en el state
        
        return authUrl.Build().ToString();
    }

    public async Task<bool> ExchangeCodeForTokensAsync(string code, Guid userId)
    {
        try
        {
            var flow = CreateGoogleAuthorizationCodeFlow();
            var tokenResponse = await flow.ExchangeCodeForTokenAsync(
                userId.ToString(),
                code,
                _redirectUri,
                CancellationToken.None);

            var googleToken = new GoogleToken
            {
                UserId = userId,
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken,
                TokenType = tokenResponse.TokenType,
                ExpiresInSeconds = tokenResponse.ExpiresInSeconds,
                IssuedUtc = tokenResponse.IssuedUtc,
                Scope = tokenResponse.Scope
            };

            await _tokenRepository.UpsertAsync(googleToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<string?> CreateEventAsync(Guid userId, CalendarEventRequest eventRequest)
    {
        var calendarService = await GetCalendarServiceAsync(userId);
        if (calendarService == null) return null;

        var newEvent = new Event
        {
            Summary = eventRequest.Title,
            Description = eventRequest.Description,
            Location = eventRequest.Location,
            Start = new EventDateTime
            {
                DateTimeDateTimeOffset = eventRequest.StartDateTime,
                TimeZone = "America/Lima"
            },
            End = new EventDateTime
            {
                DateTimeDateTimeOffset = eventRequest.EndDateTime,
                TimeZone = "America/Lima"
            }
        };

        // Agregar asistentes si los hay
        if (eventRequest.Attendees?.Any() == true)
        {
            newEvent.Attendees = eventRequest.Attendees
                .Select(email => new EventAttendee { Email = email })
                .ToList();
        }

        var request = calendarService.Events.Insert(newEvent, "primary");
        var createdEvent = await request.ExecuteAsync();

        return createdEvent.Id;
    }

    public async Task<IEnumerable<CalendarEventResponse>> GetUpcomingEventsAsync(Guid userId, int maxResults = 10)
    {
        var calendarService = await GetCalendarServiceAsync(userId);
        if (calendarService == null) return Enumerable.Empty<CalendarEventResponse>();

        var request = calendarService.Events.List("primary");
        request.TimeMinDateTimeOffset = DateTime.UtcNow;
        request.ShowDeleted = false;
        request.SingleEvents = true;
        request.MaxResults = maxResults;
        request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

        var events = await request.ExecuteAsync();

        return events.Items?.Select(e => new CalendarEventResponse
        {
            Id = e.Id,
            Title = e.Summary ?? "Sin título",
            Description = e.Description,
            StartDateTime = e.Start?.DateTimeDateTimeOffset?.DateTime,
            EndDateTime = e.End?.DateTimeDateTimeOffset?.DateTime,
            Location = e.Location,
            HtmlLink = e.HtmlLink
        }) ?? Enumerable.Empty<CalendarEventResponse>();
    }

    public async Task<bool> DeleteEventAsync(Guid userId, string eventId)
    {
        try
        {
            var calendarService = await GetCalendarServiceAsync(userId);
            if (calendarService == null) return false;

            await calendarService.Events.Delete("primary", eventId).ExecuteAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> IsConnectedAsync(Guid userId)
    {
        return await _tokenRepository.UserHasTokenAsync(userId);
    }

    public async Task<bool> DisconnectAsync(Guid userId)
    {
        try
        {
            var token = await _tokenRepository.GetByUserIdAsync(userId);
            if (token == null) return false;

            await _tokenRepository.DeleteAsync(token);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private GoogleAuthorizationCodeFlow CreateGoogleAuthorizationCodeFlow()
    {
        return new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = new ClientSecrets
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret
            },
            Scopes = new[] { CalendarService.Scope.Calendar }
        });
    }

    private async Task<CalendarService?> GetCalendarServiceAsync(Guid userId)
    {
        var googleToken = await _tokenRepository.GetByUserIdAsync(userId);
        if (googleToken == null) return null;

        var tokenResponse = new TokenResponse
        {
            AccessToken = googleToken.AccessToken,
            RefreshToken = googleToken.RefreshToken,
            TokenType = googleToken.TokenType,
            ExpiresInSeconds = googleToken.ExpiresInSeconds,
            IssuedUtc = googleToken.IssuedUtc,
            Scope = googleToken.Scope
        };

        var flow = CreateGoogleAuthorizationCodeFlow();
        var credential = new UserCredential(flow, userId.ToString(), tokenResponse);

        // Verificar si el token necesita ser refrescado
        if (credential.Token.IsStale)
        {
            await credential.RefreshTokenAsync(CancellationToken.None);
            
            // Actualizar el token en la base de datos
            googleToken.AccessToken = credential.Token.AccessToken;
            googleToken.ExpiresInSeconds = credential.Token.ExpiresInSeconds;
            googleToken.IssuedUtc = credential.Token.IssuedUtc;
            await _tokenRepository.UpsertAsync(googleToken);
        }

        return new CalendarService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "ArcheryAcademy"
        });
    }
}
