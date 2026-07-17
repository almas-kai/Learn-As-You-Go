using Application.Abstractions.Email;
using Infrastructure.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services;

public sealed class SmtpEmailService : IEmailService
{
    private readonly SmtpSettings _settings;
    private readonly ILogger<SmtpEmailService> _logger;

    public SmtpEmailService(IOptions<SmtpSettings> settings, ILogger<SmtpEmailService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(EmailMessage message, CancellationToken cancellationToken = default)
    {
        var mimeMessage = BuildMimeMessage(message);

        using var client = new SmtpClient();

        try
        {
            var secureSocketOptions = _settings.UseSsl
                ? SecureSocketOptions.SslOnConnect
                : SecureSocketOptions.None;

            await client.ConnectAsync(_settings.Host, _settings.Port, secureSocketOptions, cancellationToken);

            if (!string.IsNullOrEmpty(_settings.Username) && !string.IsNullOrEmpty(_settings.Password))
            {
                await client.AuthenticateAsync(_settings.Username, _settings.Password, cancellationToken);
            }

            await client.SendAsync(mimeMessage, cancellationToken);

            _logger.LogInformation(
                "Email sent successfully to '{To}' with subject '{Subject}'.",
                message.To, message.Subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Failed to send email to '{To}' with subject '{Subject}'.",
                message.To, message.Subject);
            throw;
        }
        finally
        {
            await client.DisconnectAsync(quit: true, cancellationToken);
        }
    }

    private MimeMessage BuildMimeMessage(EmailMessage message)
    {
        var mimeMessage = new MimeMessage();

        mimeMessage.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));
        mimeMessage.To.Add(MailboxAddress.Parse(message.To));
        mimeMessage.Subject = message.Subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = message.HtmlBody,
            TextBody = message.PlainTextBody
        };

        mimeMessage.Body = bodyBuilder.ToMessageBody();

        return mimeMessage;
    }
}
