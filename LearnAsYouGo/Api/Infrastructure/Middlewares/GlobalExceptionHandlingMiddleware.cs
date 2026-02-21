using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Infrastructure.Helpers;

namespace Api.Infrastructure.Middlewares;

public class GlobalExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleException(exception, context);
        }
    }

    private static async Task HandleException(Exception exception, HttpContext context)
    {
        var error = ApiExceptionHandler.Handle(exception);

        var jsonSerializerOptions = new JsonSerializerOptions
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var serializedError = JsonSerializer.Serialize(error, jsonSerializerOptions);

        context.Response.StatusCode = error.Status;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(serializedError);
    }
}
