using Application.Abstractions.Identity;
using Application.Exceptions;
using MediatR;

namespace Application.CQRS.Auth.Commands.ConfirmEmail;

public record ConfirmEmailCommand(string UserId, string Code) : IRequest;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IIdentityService _identityService;

    public ConfirmEmailCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var (succeeded, error) = await _identityService.ConfirmEmailAsync(request.UserId, request.Code);

        if (!succeeded)
        {
            throw new BadRequestException(error ?? "Error confirming your email.");
        }
    }
}
