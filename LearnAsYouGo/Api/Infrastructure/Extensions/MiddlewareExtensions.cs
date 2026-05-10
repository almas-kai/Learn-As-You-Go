using Api.Infrastructure.Middlewares;

namespace Api.Infrastructure.Extensions;

internal static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this WebApplication app)
    {
        return app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}
