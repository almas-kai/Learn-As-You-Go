using Application.Abstractions.Email;
using Application.Abstractions.Identity;
using Application.Exceptions;
using MediatR;
using Shared.Constants;

namespace Application.CQRS.Auth.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email, 
    string Password, 
    string? Country,
    DateTime? DateOfBirth,
    string? PhoneNumber,
    string ConfirmationUrlFormat) : IRequest;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;

    public RegisterCommandHandler(
        IIdentityService identityService,
        IEmailService emailService)
    {
        _identityService = identityService;
        _emailService = emailService;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var (succeeded, errors, userId) = await _identityService.CreateUserAsync(
            request.Email, 
            request.Password,
            request.FirstName,
            request.LastName,
            request.Country,
            request.DateOfBirth,
            request.PhoneNumber,
            AppRoles.Guest);

        if (!succeeded)
        {
            if (errors.Any(e => e.Contains("already exists", StringComparison.OrdinalIgnoreCase)))
            {
                throw new ConflictException("User with this email already exists.");
            }
            throw new BadRequestException($"User registration failed: {string.Join(", ", errors)}");
        }

        // Generate email confirmation token
        var code = await _identityService.GenerateEmailConfirmationTokenAsync(userId);

        // Create confirmation link using the provided format
        var callbackUrl = string.Format(request.ConfirmationUrlFormat, userId, code);

        // Send email
        await _emailService.SendEmailAsync(new EmailMessage(
            To: request.Email,
            Subject: "Confirm your email",
            HtmlBody: $"""
                <h2>Welcome to LearnAsYouGo!</h2>
                <p>Please confirm your email address by clicking the link below:</p>
                <p><a href="{callbackUrl}" style="padding:10px 20px;background:#4f46e5;color:white;border-radius:6px;text-decoration:none;">Confirm Email</a></p>
                <p>If you didn't create an account, you can safely ignore this email.</p>
                """,
            PlainTextBody: $"Confirm your email by visiting: {callbackUrl}"
        ));
    }
}
