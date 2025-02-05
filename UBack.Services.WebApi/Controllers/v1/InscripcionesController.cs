using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UBack.Aplication.Dtos;
using UBack.Aplication.UseCases;

namespace UBack.Services.WebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class InscripcionesController(InscripcionUseCases inscripcionUseCases) : ControllerBase
    {
        private readonly InscripcionUseCases _inscripcionUseCases = inscripcionUseCases;

        /// <summary>
        /// Método para crear un nuevo Inscripcion.
        /// </summary>
        /// <param name="InscripcionDto"></param>
        /// <returns></returns>
        [HttpPost("AddInscripcion")]
        public async Task<IActionResult> AddInscripcion([FromBody] InscripcionDTO InscripcionDto)
        {
            if (InscripcionDto == null)
                return BadRequest("El Inscripcion no puede ser nulo.");

            var response = await _inscripcionUseCases.AddInscripcionAsync(InscripcionDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Método para eliminar un Inscripcion por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteInscripcion/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteInscripcion(int id)
        {
            var response = await _inscripcionUseCases.DeleteInscripcionAsync(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Método para obtener todos los Inscripcions.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllInscripcions")]
        [Authorize]
        public async Task<IActionResult> GetAllInscripcions()
        {
            var response = await _inscripcionUseCases.GetAllInscripcionsAsync();
            return Ok(response);
        }

        /// <summary>
        /// Método para obtener un Inscripcion por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetInscripcionById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetInscripcionById(int id)
        {
            var response = await _inscripcionUseCases.GetInscripcionByIdAsync(id);
            if (response == null)
                return NotFound("Inscripcion no encontrado.");

            return Ok(response);
        }

        /// <summary>
        /// Método para actualizar un Inscripcion existente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="InscripcionDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateInscripcion/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateInscripcion(int id, [FromBody] InscripcionDTO InscripcionDto)
        {
            if (InscripcionDto == null)
                return BadRequest("El Inscripcion no puede ser nulo.");

            InscripcionDto.Id = id;
            var response = await _inscripcionUseCases.UpdateInscripcionAsync(id, InscripcionDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
    }
}