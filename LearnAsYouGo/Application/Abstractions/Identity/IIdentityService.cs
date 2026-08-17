namespace Application.Abstractions.Identity;

public interface IIdentityService
{
    Task<(bool Succeeded, IEnumerable<string> Errors, string UserId)> CreateUserAsync(
        string email, string password, string role);
    Task<string> GenerateEmailConfirmationTokenAsync(string userId);
    Task<(bool Succeeded, string? Error)> ConfirmEmailAsync(string userId, string code);
    Task<(string? UserId, bool EmailConfirmed)> GetUserStatusByEmailAsync(string email);
}
