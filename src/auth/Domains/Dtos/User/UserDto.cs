using AuthApi.Domains.Models.Enum;

namespace AuthApi.Domains.Dtos.User
{
    public record UserDto(string Id, string Login, TypeAuthorization Authorization, DateTime CreateDate);
}
