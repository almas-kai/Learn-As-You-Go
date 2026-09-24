using Application.Models.Responses.Users;
using Domain.Entities;
using Mapster;

namespace Application.Mappings;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AppUser, UserResponse>()
            // Optional: you can add specific mapping rules here in the future
            // .Map(dest => dest.UserName, src => src.Email)
            .RequireDestinationMemberSource(true);
    }
}
