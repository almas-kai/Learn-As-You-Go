namespace NettoRetail.Infrastructure.Options;

public sealed class CorsOptions
{
    public string[] AllowedOrigins { get; set; } = [];
}
