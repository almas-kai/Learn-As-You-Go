using DataAccess.Seeders.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts;

internal class AppDbContext : DbContext
{
    private readonly SeederRunner _seederRunner;
    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        SeederRunner seederRunner
    ) : base(options)
    {
        _seederRunner = seederRunner;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                await _seederRunner.RunAsync((AppDbContext)context, cancellationToken);
            })
            .UseSeeding((context, _) =>
            {
                _seederRunner.Run((AppDbContext)context);
            });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}