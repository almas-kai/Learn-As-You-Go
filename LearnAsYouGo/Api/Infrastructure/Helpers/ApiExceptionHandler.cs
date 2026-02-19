using System.Net;
using Api.Models.Error;
using Api.Models.Response;
using Application.Exceptions;

namespace Api.Infrastructure.Helpers;

public static class ApiExceptionHandler
{
    private static readonly Dictionary<Type, Func<Exception, ApiExceptionHandlerResponse>> Handlers =
    new()
    {
        { typeof(NotFoundException), exception => HandleNotFoundException((NotFoundException) exception) },
        { typeof(ConflictException), exception => HandleConflictException((ConflictException) exception) },
        { typeof(BadRequestException), exception => HandleBadRequestException((BadRequestException) exception) },
        { typeof(UnauthorizedException), exception => HandleUnautorizedException((UnauthorizedException) exception) },
        { typeof(ForbiddenException), exception => HandleForbiddedException((ForbiddenException) exception) },
    };

    public static ApiExceptionHandlerResponse Handle(Exception exception)
    {
        return Handlers.TryGetValue(exception.GetType(), out var handler)
            ? handler(exception)
            : HandleDefault(exception);
    }

    private static ApiExceptionHandlerResponse HandleDefault(Exception exception)
    {
        return new ApiExceptionHandlerResponse(
            (int)HttpStatusCode.InternalServerError,
            GenerateResponse("Unexpected error happend on the server while trying to process the request"));
    }

    private static ApiExceptionHandlerResponse HandleNotFoundException(NotFoundException exception)
    {
        return new ApiExceptionHandlerResponse((int) HttpStatusCode.NotFound, GenerateResponse(exception.Message));
    }
    private static ApiExceptionHandlerResponse HandleConflictException(ConflictException exception)
    {
        return new ApiExceptionHandlerResponse((int)HttpStatusCode.Conflict, GenerateResponse(exception.Message));
    }
    private static ApiExceptionHandlerResponse HandleBadRequestException(BadRequestException exception)
    {
        return new ApiExceptionHandlerResponse((int)HttpStatusCode.BadRequest, GenerateResponse(exception.Message));
    }
    private static ApiExceptionHandlerResponse HandleUnautorizedException(UnauthorizedException exception)
    {
        return new ApiExceptionHandlerResponse((int)HttpStatusCode.Unauthorized, GenerateResponse(exception.Message));
    }
    private static ApiExceptionHandlerResponse HandleForbiddedException(ForbiddenException exception)
    {
        return new ApiExceptionHandlerResponse((int)HttpStatusCode.Forbidden, GenerateResponse(exception.Message));
    }

    private static ApiResponse GenerateResponse(string massage) => new(massage);
}
