using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Application.Exceptions;

namespace Api.Infrastructure.ExceptionHandling;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = exception switch
        {
            NotFoundException e => CreateProblemDetails(StatusCodes.Status404NotFound, "Not Found", e.Message),
            ConflictException e => CreateProblemDetails(StatusCodes.Status409Conflict, "Conflict", e.Message),
            BadRequestException e => CreateProblemDetails(StatusCodes.Status400BadRequest, "Bad Request", e.Message),
            UnauthorizedException e => CreateProblemDetails(StatusCodes.Status401Unauthorized, "Unauthorized", e.Message),
            ForbiddenException e => CreateProblemDetails(StatusCodes.Status403Forbidden, "Forbidden", e.Message),
            System.ComponentModel.DataAnnotations.ValidationException e => CreateValidationProblemDetails(e),
            _ => CreateProblemDetails(StatusCodes.Status500InternalServerError, "Internal Server Error", "Unexpected error happened on the server while trying to process the request.")
        };

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        
        // Use WriteAsJsonAsync so it automatically sets the correct ProblemDetails content type
        await httpContext.Response.WriteAsJsonAsync(problemDetails, problemDetails.GetType(), cancellationToken: cancellationToken);

        return true;
    }

    private static ProblemDetails CreateProblemDetails(int statusCode, string title, string detail)
    {
        return new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Type = $"https://httpstatuses.com/{statusCode}"
        };
    }

    private static ProblemDetails CreateValidationProblemDetails(System.ComponentModel.DataAnnotations.ValidationException ex)
    {
        var key = ex.ValidationResult?.MemberNames?.FirstOrDefault()?.ToLowerInvariant() ?? "model";
        var errors = new Dictionary<string, string[]>
        {
            { key, new[] { ex.ValidationResult?.ErrorMessage ?? ex.Message } }
        };

        return new ValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation failed",
            Detail = "One or more validation errors occurred.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };
    }
}
