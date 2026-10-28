using Application.CQRS.Users.Queries.GetUsers;
using Application.Models.Responses.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetUsers()
    {
        var users = await _sender.Send(new GetUsersQuery());
        return Ok(users);
    }
}
