using AuthApi.Domains.Dtos.Auth;
using AuthApi.Domains.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService productService)
        {
            _service = productService;
        }

        /// <summary>
        /// Recurso responsável por realizar o login e obter o token de acesso
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        /// {
        ///     "login": "edu",
        ///     "password": "123"
        /// }
        /// 
        /// </remarks>
        /// <param name="dto">Json com os dados do login</param>
        /// <returns>Código do status da execução</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(AuthDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.LoginAsync(dto);

                if(result.HasError)
                    return Unauthorized(result);

                return Ok(result);
            }

            return BadRequest();
        }
    }
}
