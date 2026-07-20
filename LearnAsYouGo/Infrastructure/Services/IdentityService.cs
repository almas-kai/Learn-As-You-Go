using System.Text;
using Application.Abstractions.Identity;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Shared.Constants;

namespace Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<AppUser> _userManager;

    public IdentityService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors, string UserId)> CreateUserAsync(
        string email, string password, string firstName, string lastName, string? country, DateTime? dateOfBirth, string? phoneNumber, string role)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser != null)
        {
            return (false, new[] { "User with this email already exists." }, string.Empty);
        }

        var user = new AppUser 
        { 
            UserName = email, 
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Country = country,
            DateOfBirth = dateOfBirth,
            PhoneNumber = phoneNumber
        };
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description), string.Empty);
        }

        await _userManager.AddToRoleAsync(user, role);

        return (true, Enumerable.Empty<string>(), user.Id);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new InvalidOperationException($"User with ID '{userId}' not found.");
        }

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
    }

    public async Task<(bool Succeeded, string? Error)> ConfirmEmailAsync(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return (false, $"Unable to load user with ID '{userId}'.");
        }

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);
        
        if (!result.Succeeded)
        {
            return (false, "Error confirming your email.");
        }

        if (await _userManager.IsInRoleAsync(user, AppRoles.Guest))
        {
            await _userManager.RemoveFromRoleAsync(user, AppRoles.Guest);
            await _userManager.AddToRoleAsync(user, AppRoles.User);
        }

        return (true, null);
    }

    public async Task<(string? UserId, bool EmailConfirmed)> GetUserStatusByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return (null, false);
        }

        return (user.Id, user.EmailConfirmed);
    }
}
