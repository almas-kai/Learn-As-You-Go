using Domain.Entities;
using DataAccess.Seeders.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataAccess.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options
    ) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                var seederRunner = context.GetService<SeederRunner>();
                await seederRunner.RunAsync((AppDbContext)context, cancellationToken);
            })
            .UseSeeding((context, _) =>
            {
                var seederRunner = context.GetService<SeederRunner>();
                seederRunner.Run((AppDbContext)context);
            });
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}