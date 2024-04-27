using AuthApi.Domains.Base;
using AuthApi.Domains.Dtos.User;

namespace AuthApi.Domains.Interfaces
{
    public interface IUserService
    {
        Task<ApiResult<UserDto>> CreateAsync(UserCreateDto dto);
        Task<ApiResult> DeleteAsync(string id);
        Task<ApiResult<List<UserDto>>> GetAllAsync();
    }
}
