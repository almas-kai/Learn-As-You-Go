namespace Application.Abstractions.Email;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage message, CancellationToken cancellationToken = default);
}
