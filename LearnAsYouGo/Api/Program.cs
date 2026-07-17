using Api.Infrastructure.ExceptionHandling;
using Api.Infrastructure.Extensions;
using DataAccess.Contexts;
using DataAccess.Extensions;
using DataAccess.Seeders.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

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

        builder.Services.AddIdentityApiEndpoints<IdentityUser>()
            .AddEntityFrameworkStores<AppDbContext>();

        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        var app = builder.Build();

        app.MapIdentityApi<IdentityUser>();

        if (app.Environment.IsDevelopment())
        {
            await app.Services.InitializeDatabaseAsync();
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();
        app.UseExceptionHandler();
        app.UseRouting();

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        await app.RunAsync();
    }
}
