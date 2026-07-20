namespace Application.Abstractions.Identity;

public interface IIdentityService
{
    Task<(bool Succeeded, IEnumerable<string> Errors, string UserId)> CreateUserAsync(
        string email, string password, string firstName, string lastName, string? country, DateTime? dateOfBirth, string? phoneNumber, string role);
    Task<string> GenerateEmailConfirmationTokenAsync(string userId);
    Task<(bool Succeeded, string? Error)> ConfirmEmailAsync(string userId, string code);
}
