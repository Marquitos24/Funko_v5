using Funkos.Repositories;
using Funkos.Service;

namespace Funkos.Infrastructure;

public static class ApplicationInfraestructure
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Aquí registras tus repos, servicios y los Scoped/Singleton/Transient
        services.AddScoped<IFunkoRepository, FunkoRepository>();
        services.AddScoped<IFunkoService, FunkoService>();

        return services;
    }
}