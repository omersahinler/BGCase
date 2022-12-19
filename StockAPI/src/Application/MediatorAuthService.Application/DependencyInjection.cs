using FluentValidation;
using StockAPI.Application.Dtos.ConfigurationDtos;
using StockAPI.Application.Extensions;
using StockAPI.Application.PipelineBehaviours;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace StockAPI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(assembly);

        services.AddAutoMapper(assembly);

        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.Configure<MediatorTokenOptions>(configuration.GetSection("JwtTokenOption"));

        var tokenOption = configuration.GetSection("JwtTokenOption").Get<MediatorTokenOptions>();
        services.AddMediatorJwtAuth(tokenOption);

        return services;
    }
}