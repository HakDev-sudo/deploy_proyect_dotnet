using ArcheryAcademy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        //unitofwork

        return services;
    }
}