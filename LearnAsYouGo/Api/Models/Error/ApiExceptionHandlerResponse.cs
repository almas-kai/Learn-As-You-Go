using Api.Models.Response;

namespace Api.Models.Error;

public record ApiExceptionHandlerResponse(int Status, ApiResponse Result);

