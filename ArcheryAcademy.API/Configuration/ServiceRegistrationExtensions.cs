using ArcheryAcademy.Infrastructure.Configuration;

namespace ArcheryAcademy.API.Configuration;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        // Habilitar controladores de la API
        services.AddControllers();
        // Registra HttpContextAccessor (común para obtener info del request)
        services.AddHttpContextAccessor();
        
        // Habilitar Swagger
        services.AddEndpointsApiExplorer();
        
        // Registrar servicios de la capa Infrastructure
        services.AddInfrastructureServices(configuration);
        
        
        return services;
    }
}