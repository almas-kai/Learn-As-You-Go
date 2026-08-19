using Application.Abstractions.Data;
using Application.Models.Responses.Users;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.CQRS.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<List<UserResponse>>;

public class GetUsersQueryHandler(IRepository<AppUser> userRepository) : IRequestHandler<GetUsersQuery, List<UserResponse>>
{
    public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        // Here we read directly using IRepository which operates via EF Core
        var users = await userRepository.ListAsync(cancellationToken);

        return users.Adapt<List<UserResponse>>();
    }
}

