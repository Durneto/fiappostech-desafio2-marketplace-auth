using AuthApi.Domains.Models.Enum;

namespace AuthApi.Domains.Dtos.User
{
    public record UserCreateDto(string Login, string Password, TypeAuthorization Authorization);
}
