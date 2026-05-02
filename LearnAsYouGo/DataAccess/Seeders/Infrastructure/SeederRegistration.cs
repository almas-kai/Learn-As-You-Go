using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Seeders.Infrastructure;

internal static class SeederRegistration
{
    public static void RegisterSeeders(IServiceCollection services)
    {
        IEnumerable<Type> seederTypes = typeof(SeederRegistration).Assembly
            .GetTypes()
            .Where(type => typeof(IDataSeeder).IsAssignableFrom(type)
                && type is { IsInterface: false, IsAbstract: false });

        foreach(Type type in seederTypes)
        {
            services.AddScoped(typeof(IDataSeeder), type);
        }

        services.AddScoped<SeederRunner>();
    }
}