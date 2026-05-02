using Api.Models.Response;

namespace Api.Models.Error;

internal record ApiExceptionHandlerResponse(int Status, ApiResponse Result);

