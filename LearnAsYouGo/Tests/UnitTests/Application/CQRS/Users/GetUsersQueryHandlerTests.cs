using Application.Abstractions.Data;
using Application.CQRS.Users.Queries.GetUsers;
using Domain.Entities;
using NSubstitute;
using Xunit;

namespace Tests.UnitTests.Application.CQRS.Users;

public class GetUsersQueryHandlerTests
{
    private readonly IRepository<AppUser> _userRepositoryMock;
    private readonly GetUsersQueryHandler _handler;

    public GetUsersQueryHandlerTests()
    {
        _userRepositoryMock = Substitute.For<IRepository<AppUser>>();
        _handler = new GetUsersQueryHandler(_userRepositoryMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedUsers_WhenUsersExist()
    {
        // Arrange
        var users = new List<AppUser>
        {
            new AppUser { Id = "1", Email = "test1@example.com", UserName = "test1" },
            new AppUser { Id = "2", Email = "test2@example.com", UserName = "test2" }
        };

        _userRepositoryMock.ListAsync(Arg.Any<CancellationToken>()).Returns(users);

        var query = new GetUsersQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        
        Assert.Equal("1", result[0].Id);
        Assert.Equal("test1@example.com", result[0].Email);
        Assert.Equal("test1", result[0].UserName);

        Assert.Equal("2", result[1].Id);
        Assert.Equal("test2@example.com", result[1].Email);
        Assert.Equal("test2", result[1].UserName);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        _userRepositoryMock.ListAsync(Arg.Any<CancellationToken>()).Returns(new List<AppUser>());

        var query = new GetUsersQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
