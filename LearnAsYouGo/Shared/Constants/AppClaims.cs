namespace Shared.Constants;

public static class AppClaims
{
    public const string Permission = "Permission";

    public static class Permissions
    {
        public const string All = "All";
        public const string Basic = "Basic";
        public const string None = "None"; // For guests or restricted users
    }
}
