using System.Runtime.CompilerServices;
using Application;
using Mapster;

namespace Tests.UnitTests;

public static class TestSetup
{
    [ModuleInitializer]
    public static void InitializeMapster()
    {
        // This will automatically run once before any tests in this assembly are executed.
        // It ensures Mapster is configured exactly like it is in the real application.
        TypeAdapterConfig.GlobalSettings.Scan(typeof(DependencyInjection).Assembly);
    }
}
