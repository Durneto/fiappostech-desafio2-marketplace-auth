using AuthApi.Domains.Base;
using AuthApi.Domains.Dtos.Auth;
using AuthApi.Domains.Interfaces;
using AuthApi.Infra.Repositorys.Base;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUow _uow;
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository repository, IUow uow, IConfiguration configuration)
        {
            _uow = uow;
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<ApiResult<AuthCredentialsDto>> LoginAsync(AuthDto dto)
        {
            var result = new ApiResult<AuthCredentialsDto>();
            try
            {
                _uow.Open();

                var model = await _repository.GetLoginAsync(dto.Login);
                
                if (model == null || dto.Password != model.Password)
                    result.Erros.Add("Usuário ou senha incorretos");

                if (!result.HasError)
                {
                    result.Data = new AuthCredentialsDto(dto.Login, "token bla bla bla", DateTime.Now.AddMinutes(30));

                    //Gerando token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["SecretJWT"]);

                    var tokenProperts = new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(new Claim[] 
                        {
                            new Claim(ClaimTypes.Name, model.Login),
                            new Claim(ClaimTypes.Role, model.Authorization.ToString()),
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(2),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenProperts);
                    result.Data.Token = tokenHandler.WriteToken(token);
                }
            }
            catch (Exception ex)
            {
                result.Erros.Add(ex.ToString());
            }
            finally
            {
                _uow.Dispose();
            }

            return result;
        }
    }
}
