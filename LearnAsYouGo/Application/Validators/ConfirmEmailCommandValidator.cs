using Application.CQRS.Auth.Commands.ConfirmEmail;
using FluentValidation;

namespace Application.Validators;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(v => v.Code)
            .NotEmpty().WithMessage("Code is required.");
    }
}
