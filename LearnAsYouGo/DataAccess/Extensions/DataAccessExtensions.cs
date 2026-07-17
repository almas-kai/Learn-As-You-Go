using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Contexts;
using DataAccess.Seeders;
using DataAccess.Seeders.Infrastructure;
using Application.Abstractions.Data;
using DataAccess.Repositories;
using DataAccess.UnitOfWork;

namespace DataAccess.Extensions;

public static class DataAccessExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureConnectionString(services, configuration);

        SeederRegistration.RegisterSeeders(services);

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, DataAccess.UnitOfWork.UnitOfWork>();

        return services;
    }

    public static async Task InitializeDatabaseAsync(this IServiceProvider services)
    {
        await using AsyncServiceScope scope = services.CreateAsyncScope();
        AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();

        await IdentitySeeder.SeedAsync(scope.ServiceProvider);
    }

    private static void ConfigureConnectionString(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("The connection string wasn't found.");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }
}