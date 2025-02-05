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
    public class MateriasController(MateriaUseCases materiaUseCases) : ControllerBase
    {
        private readonly MateriaUseCases _materiaUseCases = materiaUseCases;
        /// <summary>
        /// Método para crear un nuevo Materia.
        /// </summary>
        /// <param name="MateriaDto"></param>
        /// <returns></returns>
        [HttpPost("AddMateria")]
        public async Task<IActionResult> AddMateria([FromBody] MateriaDTO MateriaDto)
        {
            if (MateriaDto == null)
                return BadRequest("El Materia no puede ser nulo.");

            var response = await _materiaUseCases.AddMateriaAsync(MateriaDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Método para eliminar un Materia por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteMateria/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMateria(int id)
        {
            var response = await _materiaUseCases.DeleteMateriaAsync(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Método para obtener todos los Materias.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllMaterias")]
        [Authorize]
        public async Task<IActionResult> GetAllMaterias()
        {
            var response = await _materiaUseCases.GetAllMateriasAsync();
            return Ok(response);
        }

        /// <summary>
        /// Método para obtener un Materia por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetMateriaById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetMateriaById(int id)
        {
            var response = await _materiaUseCases.GetMateriaByIdAsync(id);
            if (response == null)
                return NotFound("Materia no encontrado.");

            return Ok(response);
        }

        /// <summary>
        /// Método para actualizar un Materia existente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="MateriaDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateMateria/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateMateria(int id, [FromBody] MateriaDTO MateriaDto)
        {
            if (MateriaDto == null)
                return BadRequest("El Materia no puede ser nulo.");

            MateriaDto.Id = id;
            var response = await _materiaUseCases.UpdateMateriaAsync(id, MateriaDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
    }
}
