using Application.Abstractions.Email;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Infrastructure.Services;

/// <summary>
/// Adapter that bridges ASP.NET Core Identity's IEmailSender{TUser} interface
/// to our application-level IEmailService. This enables Identity features like
/// email confirmation, password reset links, etc.
/// </summary>
public sealed class IdentityEmailSender : IEmailSender<AppUser>
{
    private readonly IEmailService _emailService;

    public IdentityEmailSender(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public Task SendConfirmationLinkAsync(AppUser user, string email, string confirmationLink) =>
        _emailService.SendEmailAsync(new EmailMessage(
            To: email,
            Subject: "Confirm your email",
            HtmlBody: $"""
                <h2>Welcome to LearnAsYouGo!</h2>
                <p>Please confirm your email address by clicking the link below:</p>
                <p><a href="{confirmationLink}" style="padding:10px 20px;background:#4f46e5;color:white;border-radius:6px;text-decoration:none;">Confirm Email</a></p>
                <p>If you didn't create an account, you can safely ignore this email.</p>
                """,
            PlainTextBody: $"Confirm your email by visiting: {confirmationLink}"
        ));

    public Task SendPasswordResetLinkAsync(AppUser user, string email, string resetLink) =>
        _emailService.SendEmailAsync(new EmailMessage(
            To: email,
            Subject: "Reset your password",
            HtmlBody: $"""
                <h2>Password Reset</h2>
                <p>You requested a password reset. Click the link below to proceed:</p>
                <p><a href="{resetLink}" style="padding:10px 20px;background:#4f46e5;color:white;border-radius:6px;text-decoration:none;">Reset Password</a></p>
                <p>If you didn't request this, you can safely ignore this email.</p>
                """,
            PlainTextBody: $"Reset your password by visiting: {resetLink}"
        ));

    public Task SendPasswordResetCodeAsync(AppUser user, string email, string resetCode) =>
        _emailService.SendEmailAsync(new EmailMessage(
            To: email,
            Subject: "Your password reset code",
            HtmlBody: $"""
                <h2>Password Reset Code</h2>
                <p>Your password reset code is:</p>
                <p style="font-size:2rem;font-weight:bold;letter-spacing:4px;color:#4f46e5;">{resetCode}</p>
                <p>This code expires shortly. If you didn't request this, ignore this email.</p>
                """,
            PlainTextBody: $"Your password reset code: {resetCode}"
        ));
}
