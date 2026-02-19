namespace Api.Models.Response;

public record ApiResponse(string Massage, Dictionary<string, string[]>? Errors = null);
public record ApiResponse<TModel>(TModel Data);
