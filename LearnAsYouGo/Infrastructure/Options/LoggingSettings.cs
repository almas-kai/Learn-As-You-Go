namespace Infrastructure.Options;

public sealed class LoggingSettings
{
    public const string SectionName = "LoggingSettings";

    public string ConnectionString { get; set; } = string.Empty;
    public string TableName { get; set; } = "Logs";
}
