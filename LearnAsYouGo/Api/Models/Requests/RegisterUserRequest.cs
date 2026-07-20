namespace Api.Models.Requests;

public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email, 
    string Password,
    string? Country,
    DateTime? DateOfBirth,
    string? PhoneNumber);
