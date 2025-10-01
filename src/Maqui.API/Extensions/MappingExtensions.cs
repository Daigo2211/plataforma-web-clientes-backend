using AutoMapper;
using Maqui.Application.Mappings;

namespace Maqui.API.Extensions;

public static class MappingExtensions
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(ClienteProfile));
        return services;
    }
}