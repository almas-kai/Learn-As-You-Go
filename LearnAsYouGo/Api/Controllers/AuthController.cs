using System.Text;
using Api.Models.Requests;
using Application.CQRS.Auth.Commands.Register;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        // Define the callback URL format for the handler to populate with userId and code
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
        
        // The handler will throw a ConflictException or BadRequestException on failure,
        // which should be caught by the global exception handler.
        await _sender.Send(command);

        return Ok(new { Message = "User registered successfully. Please check your email to confirm your account." });
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        var command = new Application.CQRS.Auth.Commands.ConfirmEmail.ConfirmEmailCommand(userId, code);
        
        // The handler will throw a BadRequestException on failure,
        // which should be caught by the global exception handler.
        await _sender.Send(command);

        return Ok("Thank you for confirming your email.");
    }
}
