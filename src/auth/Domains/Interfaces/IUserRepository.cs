using AuthApi.Domains.Models;

namespace AuthApi.Domains.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task DeleteAsync(string id);
        Task<List<User>> GetAllAsync();
        Task<User> GetAsync(string id);
        Task<User> GetLoginAsync(string login);
    }
}
