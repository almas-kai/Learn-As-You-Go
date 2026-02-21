using Api.Infrastructure.Middlewares;

namespace Api.Infrastructure.Extention;

public static class MiddlewareExtentions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this WebApplication app)
    {
        return app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}
