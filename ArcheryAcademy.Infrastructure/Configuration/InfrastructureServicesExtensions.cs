using ArcheryAcademy.Domain.Ports;
using ArcheryAcademy.Domain.Ports.Repositories;
using ArcheryAcademy.Domain.Ports.Services;
using ArcheryAcademy.Infrastructure.Adapters.Repositories;
using ArcheryAcademy.Infrastructure.Adapters.Services;
using ArcheryAcademy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ArcheryAcademy.Domain.Ports.Authentication;
using ArcheryAcademy.Infrastructure.Adapters.Authentication;

namespace ArcheryAcademy.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // 1. Conexión a la Base de Datos
        services.AddDbContext<ArcheryAcademyDbContext>((IServiceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection"); 
            options.UseNpgsql(connectionString);
        }); 
        //Repository Pattern
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserPlanRepository, UserPlanRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<ICertificateRepository, CertificateRepository>();
        services.AddScoped<IGoogleTokenRepository, GoogleTokenRepository>();
        
        // Services
        services.AddScoped<IBlobStorageService, AzureBlobStorageService>();
        services.AddScoped<ICertificateService, CertificateService>();
        services.AddScoped<IGoogleCalendarService, GoogleCalendarService>();
        
        // Authentication
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        return services;
    }
}