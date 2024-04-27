
namespace AuthApi.Domains.Dtos.Auth
{
    public class AuthCredentialsDto
    {
        public AuthCredentialsDto(string login, string token, DateTime expiration)
        {
            Login = login;
            Token = token;
            Expiration = expiration;
        }

        public string Login { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
