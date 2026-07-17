namespace Infrastructure.Options;

public sealed class SeedSettings
{
    public const string SectionName = "SeedSettings";

    public string AdminEmail { get; set; } = string.Empty;
    public string AdminPassword { get; set; } = string.Empty;
}
