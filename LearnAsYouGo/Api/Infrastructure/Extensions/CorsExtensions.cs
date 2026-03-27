using NettoRetail.Infrastructure.Options;

namespace Api.Infrastructure.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddDefaultCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                CorsOptions corsOptions = configuration
                    .GetSection(nameof(CorsOptions))
                    .Get<CorsOptions>() ?? throw new InvalidOperationException("CORS is not configured correctly. Couldn't find 'CorsOptions' in config files.");

                policy.WithOrigins(corsOptions.AllowedOrigins)
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }
}