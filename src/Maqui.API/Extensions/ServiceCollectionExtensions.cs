using FluentValidation;
using Maqui.Application.Interfaces;
using Maqui.Application.Services;
using Maqui.Application.Validators;
using Maqui.Domain.Interfaces;
using Maqui.Infrastructure.Data.Context;
using Maqui.Infrastructure.Repositories;
using Maqui.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Maqui.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IClienteService, ClienteService>();
        services.AddValidatorsFromAssemblyContaining<ClienteCreateValidator>();
        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IFileService, FileService>();
        return services;
    }

    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new[] { "http://localhost:5173" };
        services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp", builder =>
            {
                builder.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });
        });
        return services;
    }
}