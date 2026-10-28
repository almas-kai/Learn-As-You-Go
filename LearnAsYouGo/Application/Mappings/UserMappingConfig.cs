using Application.Models.Responses.Users;
using Domain.Entities;
using Mapster;

namespace Application.Mappings;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AppUser, UserResponse>()
            .RequireDestinationMemberSource(true);
    }
}
