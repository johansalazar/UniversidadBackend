using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UBack.Aplication.Dtos;
using UBack.Aplication.UseCases;

namespace UBack.Services.WebApi.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsuariosController(UsuarioUseCases usuarioUseCases) : ControllerBase
    {
        private readonly UsuarioUseCases _usuarioUseCases = usuarioUseCases;

        /// <summary>
        /// Método para crear un nuevo usuario.
        /// </summary>
        /// <param name="UsuarioDto"></param>
        /// <returns></returns>
        [HttpPost("AddUsuario")]
        public async Task<IActionResult> AddUsuario([FromBody] UsuarioDTO UsuarioDto)
        {
            if (UsuarioDto == null)
                return BadRequest("El usuario no puede ser nulo.");

            var response = await _usuarioUseCases.AddUsuarioAsync(UsuarioDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Método para eliminar un usuario por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUsuario/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var response = await _usuarioUseCases.DeleteUsuarioAsync(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Método para obtener todos los usuarios.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllUsuarios")]
        [Authorize]
        public async Task<IActionResult> GetAllUsuarios()
        {
            var response = await _usuarioUseCases.GetAllUsuariosAsync();
            return Ok(response);
        }

        /// <summary>
        /// Método para obtener un usuario por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUsuarioById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var response = await _usuarioUseCases.GetUsuarioByIdAsync(id);
            if (response == null)
                return NotFound("Usuario no encontrado.");

            return Ok(response);
        }

        /// <summary>
        /// Método para actualizar un usuario existente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UsuarioDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateUsuario/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioDTO UsuarioDto)
        {
            if (UsuarioDto == null)
                return BadRequest("El usuario no puede ser nulo.");

            UsuarioDto.Id = id;
            var response = await _usuarioUseCases.UpdateUsuarioAsync(id, UsuarioDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
    }
}

