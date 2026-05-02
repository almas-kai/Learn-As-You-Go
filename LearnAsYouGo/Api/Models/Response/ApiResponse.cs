namespace Api.Models.Response;

internal record ApiResponse(string Massage, Dictionary<string, string[]>? Errors = null);
internal record ApiResponse<TModel>(TModel Data);
