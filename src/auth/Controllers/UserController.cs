using AuthApi.Domains.Dtos.User;
using AuthApi.Domains.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService productService)
        {
            _service = productService;
        }

        /// <summary>
        /// Recurso responsável por obter todos os usuários
        /// </summary>
        /// <returns>Lista de usuários encontrados</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        /// <summary>
        /// Recurso responsável por criar um usuário
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        /// {
        ///     "login": "edu",
        ///     "password": "123",
        ///     "authorization": 1
        /// }
        /// 
        /// </remarks>
        /// <param name="dto">Json com os dados do usuário</param>
        /// <returns>Código do status da execução</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(UserCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateAsync(dto);

                if(result.HasError)
                    return BadRequest(result);

                return CreatedAtAction(nameof(GetAll), new { id = result.Data.Id }, result.Data);
            }

            return BadRequest();
        }

        /// <summary>
        /// Recurso responsável por excluir um usuário
        /// </summary>
        /// <param name="id">Código identificador do usuário</param>
        /// <returns>Código do status da execução</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteAsync(id);

            if (result.HasError)
                return BadRequest(result);

            return NoContent();
        }
    }
}
