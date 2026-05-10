using Api.Infrastructure.Extensions;
using DataAccess.Extensions;

namespace Api;

internal static class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDefaultCors(builder.Configuration);

        builder.Services.AddAuthentication();

        builder.Services.AddAuthorization();

        builder.Services.AddOpenApi();

        builder.Services.AddDataAccess(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            await app.Services.InitializeDatabaseAsync();
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseGlobalExceptionHandling();
        app.UseRouting();

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        await app.RunAsync();
    }
}
