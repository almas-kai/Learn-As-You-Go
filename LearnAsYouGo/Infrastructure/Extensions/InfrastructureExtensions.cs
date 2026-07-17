using Application.Abstractions.Email;
using Infrastructure.Options;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SmtpSettings>(
            configuration.GetSection(SmtpSettings.SectionName));

        services.AddTransient<IEmailService, SmtpEmailService>();

        return services;
    }
}
