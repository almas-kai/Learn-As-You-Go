using Api.Models.Requests;
using Application.CQRS.Auth.Commands.ConfirmEmail;
using Application.CQRS.Auth.Commands.Register;
using Application.CQRS.Auth.Commands.ResendConfirmationEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Secure by default
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var callbackUrlFormat = Url.Action(
            action: nameof(ConfirmEmail),
            controller: "Auth",
            values: new { userId = "{0}", code = "{1}" },
            protocol: Request.Scheme);

        if (callbackUrlFormat == null)
        {
            return StatusCode(500, "Could not generate confirmation URL.");
        }

        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email, 
            request.Password,
            request.Country,
            request.DateOfBirth,
            request.PhoneNumber,
            callbackUrlFormat);
        
        await _sender.Send(command);

        return Ok(new { Message = "User registered successfully. Please check your email to confirm your account." });
    }

    [HttpGet("confirm-email")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        var command = new ConfirmEmailCommand(userId, code);
        
        await _sender.Send(command);

        return Ok("Thank you for confirming your email.");
    }

    [HttpPost("resend-confirmation-email")]
    [AllowAnonymous]
    public async Task<IActionResult> ResendConfirmationEmail([FromBody] string email)
    {
        var callbackUrlFormat = Url.Action(
            action: nameof(ConfirmEmail),
            controller: "Auth",
            values: new { userId = "{0}", code = "{1}" },
            protocol: Request.Scheme);

        if (callbackUrlFormat == null)
        {
            return StatusCode(500, "Could not generate confirmation URL.");
        }

        var command = new ResendConfirmationEmailCommand(email, callbackUrlFormat);
        
        await _sender.Send(command);

        return Ok(new { message = "If your email is registered and unconfirmed, a new confirmation link has been sent." });
    }
}
