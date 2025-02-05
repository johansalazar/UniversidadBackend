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
    public class RolesController(RolUseCases RolUseCases) : ControllerBase
    {
        private readonly RolUseCases _rolUseCases = RolUseCases;
        /// <summary>
        /// Método para crear un nuevo Rol.
        /// </summary>
        /// <param name="RolDto"></param>
        /// <returns></returns>
        [HttpPost("AddRol")]
        public async Task<IActionResult> AddRol([FromBody] RolDTO RolDto)
        {
            if (RolDto == null)
                return BadRequest("El Rol no puede ser nulo.");

            var response = await _rolUseCases.AddRolAsync(RolDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Método para eliminar un Rol por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteRol/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var response = await _rolUseCases.DeleteRolAsync(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Método para obtener todos los Rols.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllRols")]
        [Authorize]
        public async Task<IActionResult> GetAllRols()
        {
            var response = await _rolUseCases.GetAllRolsAsync();
            return Ok(response);
        }

        /// <summary>
        /// Método para obtener un Rol por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetRolById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetRolById(int id)
        {
            var response = await _rolUseCases.GetRolByIdAsync(id);
            if (response == null)
                return NotFound("Rol no encontrado.");

            return Ok(response);
        }

        /// <summary>
        /// Método para actualizar un Rol existente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="RolDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateRol/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRol(int id, [FromBody] RolDTO RolDto)
        {
            if (RolDto == null)
                return BadRequest("El Rol no puede ser nulo.");

            RolDto.Id = id;
            var response = await _rolUseCases.UpdateRolAsync(id, RolDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
    }
}
