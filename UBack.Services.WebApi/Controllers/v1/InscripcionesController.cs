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
        /// Método para crear una nueva inscripción.
        /// </summary>
        [HttpPost("AddInscripcion")]
        public async Task<IActionResult> AddInscripcion([FromBody] InscripcionDTO InscripcionDto)
        {
            if (InscripcionDto == null)
                return BadRequest("La inscripción no puede ser nula.");

            var response = await _inscripcionUseCases.AddInscripcionAsync(InscripcionDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response.Message);
        }

        /// <summary>
        /// Método para eliminar una inscripción por su ID.
        /// </summary>
        [HttpDelete("DeleteInscripcion/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteInscripcion(int id)
        {
            var response = await _inscripcionUseCases.DeleteInscripcionAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response.Message);
        }

        /// <summary>
        /// Método para obtener todas las inscripciones.
        /// </summary>
        [HttpGet("GetAllInscripciones")]
        [Authorize]
        public async Task<IActionResult> GetAllInscripcions()
        {
            var response = await _inscripcionUseCases.GetAllInscripcionsAsync();
            return Ok(response);
        }

        /// <summary>
        /// Método para obtener una inscripción por su ID.
        /// </summary>
        [HttpGet("GetInscripcionById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetInscripcionById(int id)
        {
            var response = await _inscripcionUseCases.GetInscripcionByIdAsync(id);
            return response == null ? NotFound("Inscripción no encontrada.") : Ok(response);
        }

        /// <summary>
        /// Método para actualizar una inscripción existente.
        /// </summary>
        [HttpPut("UpdateInscripcion/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateInscripcion(int id, [FromBody] InscripcionDTO InscripcionDto)
        {
            if (InscripcionDto == null)
                return BadRequest("La inscripción no puede ser nula.");

            InscripcionDto.Id = id;
            var response = await _inscripcionUseCases.UpdateInscripcionAsync(id, InscripcionDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response.Message);
        }

        /// <summary>
        /// Obtener la cantidad de materias inscritas por un estudiante específico.
        /// </summary>
        [HttpGet("GetMateriasInscritasPorEstudiante/{idEstudiante}")]
        public async Task<IActionResult> GetMateriasInscritasPorEstudiante(int idEstudiante)
        {
            var response = await _inscripcionUseCases.GetMateriasInscritasPorEstudiante(idEstudiante);
            return response.IsSuccess ? Ok(response) : BadRequest(response.Message);
        }

        /// <summary>
        /// Verificar si un estudiante tiene otras materias con el mismo profesor.
        /// </summary>
        [HttpGet("GetMateriasConMismoProfesor/{idEstudiante}/{idMateria}")]
        public async Task<IActionResult> GetMateriasConMismoProfesor(int idEstudiante, int idMateria)
        {
            var response = await _inscripcionUseCases.GetMateriasConMismoProfesor(idEstudiante, idMateria);
            return response.IsSuccess ? Ok(response) : BadRequest(response.Message);
        }

        /// <summary>
        /// Listar los compañeros de clase en las materias del estudiante.
        /// </summary>
        [HttpPost("GetCompanerosPorMateria")]
        public async Task<IActionResult> GetCompanerosPorMateria([FromBody] List<int> idMaterias)
        {
            if (idMaterias == null || !idMaterias.Any())
                return BadRequest("La lista de materias no puede estar vacía.");

            var response = await _inscripcionUseCases.GetCompanerosPorMateria(idMaterias);
            return response.IsSuccess ? Ok(response) : BadRequest(response.Message);
        }
    }
}