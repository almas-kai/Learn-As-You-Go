using DataAccess.Contexts;

namespace DataAccess.Seeders.Infrastructure;

public interface IDataSeeder
{
    public Task SeedAsync(AppDbContext appDbContext, CancellationToken cancellationToken);
    public void Seed(AppDbContext appDbContext);
}