using Application.Abstractions.Email;
using Application.Abstractions.Identity;
using Application.Exceptions;
using MediatR;

namespace Application.CQRS.Auth.Commands.ResendConfirmationEmail;

public record ResendConfirmationEmailCommand(
    string Email,
    string ConfirmationUrlFormat) : IRequest;

public class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;

    public ResendConfirmationEmailCommandHandler(
        IIdentityService identityService,
        IEmailService emailService)
    {
        _identityService = identityService;
        _emailService = emailService;
    }

    public async Task Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        var (userId, emailConfirmed) = await _identityService.GetUserStatusByEmailAsync(request.Email);

        if (userId == null)
        {
            // Do not reveal that the user does not exist
            return;
        }

        if (emailConfirmed)
        {
            throw new BadRequestException("Email is already confirmed.");
        }

        var code = await _identityService.GenerateEmailConfirmationTokenAsync(userId);
        var callbackUrl = string.Format(request.ConfirmationUrlFormat, userId, code);

        await _emailService.SendEmailAsync(new EmailMessage(
            To: request.Email,
            Subject: "Confirm your email",
            HtmlBody: $"""
                <h2>Welcome to LearnAsYouGo!</h2>
                <p>Please confirm your email address by clicking the link below:</p>
                <p><a href="{callbackUrl}" style="padding:10px 20px;background:#4f46e5;color:white;border-radius:6px;text-decoration:none;">Confirm Email</a></p>
                <p>If you didn't request this email, you can safely ignore it.</p>
                """,
            PlainTextBody: $"Confirm your email by visiting: {callbackUrl}"
        ));
    }
}
