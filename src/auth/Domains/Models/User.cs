using AuthApi.Domains.Dtos.User;
using AuthApi.Domains.Models.Enum;

namespace AuthApi.Domains.Models
{
    public class User
    {
        public User()
        {
            
        }

        public User(UserCreateDto dto)
        {
            this.Login = dto.Login;
            this.Password = dto.Password;
            this.Authorization = dto.Authorization;
        }

        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public TypeAuthorization Authorization { get; set; }

        internal UserDto ToDto()
        {
            return new UserDto(Id, Login, Authorization, CreateDate);
        }
    }
}
