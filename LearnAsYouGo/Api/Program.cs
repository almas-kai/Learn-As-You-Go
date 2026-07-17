using Api.Infrastructure.ExceptionHandling;
using Api.Infrastructure.Extensions;
using Application;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Infrastructure.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using Serilog;

namespace Api;

internal static class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddStructuredLogging();

        builder.Services.AddDefaultCors(builder.Configuration);

        builder.Services.AddAuthentication();

        builder.Services.AddAuthorization();

        builder.Services.AddOpenApi();
        
        builder.Services.AddDataAccess(builder.Configuration);
        builder.Services.AddApplication();

        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddIdentityApiEndpoints<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddTransient<IEmailSender<IdentityUser>, IdentityEmailSender>();

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
        app.UseSerilogRequestLogging();
        app.UseExceptionHandler();
        app.UseRouting();

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        await app.RunAsync();
    }
}
