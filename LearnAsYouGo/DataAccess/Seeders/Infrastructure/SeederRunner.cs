using DataAccess.Contexts;

namespace DataAccess.Seeders.Infrastructure;

public class SeederRunner
{
    private readonly IEnumerable<IDataSeeder> _seeders;
    public SeederRunner(IEnumerable<IDataSeeder> seeders)
    {
        _seeders = seeders;
    }

    public async Task RunAsync(AppDbContext dbContext, CancellationToken cancellationToken)
    {
        foreach(IDataSeeder seeder in _seeders)
        {
            await seeder.SeedAsync(dbContext, cancellationToken);
        }
    }

    public void Run(AppDbContext dbContext)
    {
        foreach(IDataSeeder seeder in _seeders)
        {
            seeder.Seed(dbContext);
        }
    }
}