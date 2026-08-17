namespace Api.Models.Requests;

public record RegisterUserRequest(
    string Email, 
    string Password);
