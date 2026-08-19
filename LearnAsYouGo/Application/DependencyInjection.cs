using System.Reflection;
using Application.Behaviors;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        TypeAdapterConfig.GlobalSettings.Scan(assembly);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.LicenseKey = configuration.GetSection("LuckyPennySoftware:LicenseKey").Value ?? throw new InvalidOperationException("Couldn't find MediatR license key from user-secrets or config files.");
        });

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
