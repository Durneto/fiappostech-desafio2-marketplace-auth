using AuthApi.Domains.Base;
using AuthApi.Domains.Dtos.Auth;

namespace AuthApi.Domains.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResult<AuthCredentialsDto>> LoginAsync(AuthDto dto);
    }
}
