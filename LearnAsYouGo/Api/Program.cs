using Api.Infrastructure.Extensions;

namespace Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDefaultCors(builder.Configuration);

        builder.Services.AddAuthorization();

        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseGlobalExceptionHandling();
        app.UseRouting();

        app.UseCors();  

        app.UseAuthentication();
        app.UseAuthorization();

        app.Run();
    }
}
