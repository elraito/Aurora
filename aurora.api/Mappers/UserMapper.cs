using Aurora.Api.Dtos.User.Requests;
using Aurora.Api.Entities;

public static class UserMapper
{

    public static User MapToEntity(this CreateRequest dto) =>
        new User
        {
            Username = dto.Username,
            Role = dto.Role,
        };
}