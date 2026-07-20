using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Constants;

namespace DataAccess.Seeders;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var logger = serviceProvider.GetRequiredService<ILogger<RoleManager<IdentityRole>>>();

        await SeedRolesAsync(roleManager, logger);
        await SeedAdminUserAsync(userManager, configuration, logger);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, ILogger logger)
    {
        string[] roles = [AppRoles.Admin, AppRoles.User];

        foreach (string role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var identityRole = new IdentityRole(role);
                var result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    logger.LogInformation("Role '{Role}' created successfully.", role);

                    string permission = role == AppRoles.Admin ? AppClaims.Permissions.All : AppClaims.Permissions.Basic;
                    var claimResult = await roleManager.AddClaimAsync(identityRole, new System.Security.Claims.Claim(AppClaims.Permission, permission));

                    if (claimResult.Succeeded)
                    {
                        logger.LogInformation("Assigned claim '{ClaimType}: {ClaimValue}' to role '{Role}'.", AppClaims.Permission, permission, role);
                    }
                    else
                    {
                        logger.LogError("Failed to assign claim to role '{Role}': {Errors}", role, string.Join(", ", claimResult.Errors.Select(e => e.Description)));
                    }
                }
                else
                {
                    logger.LogError("Failed to create role '{Role}': {Errors}",
                        role, string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }

    private static async Task SeedAdminUserAsync(
        UserManager<AppUser> userManager,
        IConfiguration configuration,
        ILogger logger)
    {
        string adminEmail = configuration["SeedSettings:AdminEmail"]
            ?? throw new InvalidOperationException("SeedSettings:AdminEmail is not configured.");
        string adminPassword = configuration["SeedSettings:AdminPassword"]
            ?? throw new InvalidOperationException("SeedSettings:AdminPassword is not configured.");

        AppUser? existingAdmin = await userManager.FindByEmailAsync(adminEmail);

        if (existingAdmin is not null)
        {
            logger.LogInformation("Admin user '{Email}' already exists. Skipping.", adminEmail);
            return;
        }

        var adminUser = new AppUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "User"
        };

        var createResult = await userManager.CreateAsync(adminUser, adminPassword);

        if (!createResult.Succeeded)
        {
            logger.LogError("Failed to create admin user '{Email}': {Errors}",
                adminEmail, string.Join(", ", createResult.Errors.Select(e => e.Description)));
            return;
        }

        var roleResult = await userManager.AddToRoleAsync(adminUser, AppRoles.Admin);

        if (roleResult.Succeeded)
        {
            logger.LogInformation("Admin user '{Email}' created and assigned role '{Role}'.",
                adminEmail, AppRoles.Admin);
        }
        else
        {
            logger.LogError("Failed to assign role '{Role}' to admin user '{Email}': {Errors}",
                AppRoles.Admin, adminEmail, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
        }
    }
}
