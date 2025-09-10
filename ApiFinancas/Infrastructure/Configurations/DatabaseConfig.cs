using ApiFinancas.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiFinancas.Infrastructure.Configurations;

public static class DatabaseConfig
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddEntityFrameworkNpgsql()
            .AddDbContext<AppDbContext>(Options =>
            {
                Options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

        return service;
    }
}
