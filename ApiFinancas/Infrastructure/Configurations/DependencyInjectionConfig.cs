using AFSilva.Core.Auth;
using AFSilva.Core.Repositories.Models;
using ApiFinancas.Data;
using ApiFinancas.Domain.Interfaces;
using ApiFinancas.Services;
using Microsoft.EntityFrameworkCore;

namespace ApiFinancas.Infrastructure.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddHttpContextAccessor();
        service.AddScoped<DbContext, AppDbContext>();
        service.AddScoped(typeof(RepositoryBase<>), typeof(RepositoryBase<>));
        service.AddScoped<JwtTokenGenerator>();
        service.AddScoped<IUsuarioService, UsuarioService>();
        service.AddScoped<ILancamentoService, LancamentoService>();


        service.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins",
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "url front end hospedado")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
        });

        service.AddControllers();
        service.AddEndpointsApiExplorer();
        service.AddSwaggerConfigurarion();
        service.AddJwtAuthentication(configuration);

        return service;
    }
}
