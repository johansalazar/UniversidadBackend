using AutoMapper;
using UBack.Aplication.Dtos;
using UBack.Aplication.Interfaces;
using UBack.Domain.Dominio.Entities;
using UBack.Transversal.Common;

namespace UBack.Aplication.UseCases
{
    public class InscripcionUseCases(IInscripcionRepository InscripcionRepository, IMapper mapper)
    {
        private readonly IInscripcionRepository _InscripcionRepository = InscripcionRepository;
        private readonly IMapper _mapper = mapper;

        // Agregar un nuevo Inscripcion
        public async Task<Response<InscripcionDTO>> AddInscripcionAsync(InscripcionDTO InscripcionDto)
        {
            var response = new Response<InscripcionDTO>();
            try
            {

                // Mapeo del DTO a modelo de dominio
                var Inscripcion = _mapper.Map<Inscripcion>(InscripcionDto);

                // Agregar el Inscripcion en la base de datos
                var result = await _InscripcionRepository.AddInscripcionAsync(Inscripcion);
                if (result)
                {
                    response.Data = InscripcionDto;
                    response.IsSuccess = true;
                    response.Message = "Inscripcion creado exitosamente.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo crear el Inscripcion.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";
            }

            return response;
        }

        // Obtener un Inscripcion por ID
        public async Task<InscripcionDTO> GetInscripcionByIdAsync(int id)
        {
            var Inscripcion = await _InscripcionRepository.GetInscripcionByIdAsync(id);
            return Inscripcion != null ? _mapper.Map<InscripcionDTO>(Inscripcion) : null;
        }

        // Obtener todos los Inscripcions
        public async Task<IEnumerable<InscripcionDTO>> GetAllInscripcionsAsync()
        {
            var Inscripcions = await _InscripcionRepository.GetAllInscripcionAsync();
            return _mapper.Map<IEnumerable<InscripcionDTO>>(Inscripcions);
        }

        // Actualizar un Inscripcion
        public async Task<Response<InscripcionDTO>> UpdateInscripcionAsync(int id, InscripcionDTO InscripcionDto)
        {
            var response = new Response<InscripcionDTO>();
            try
            {
                var Inscripcion = await _InscripcionRepository.GetInscripcionByIdAsync(id);
                if (Inscripcion == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Inscripcion no encontrado.";
                    return response;
                }

                // Mapeo del DTO al modelo de dominio
                _mapper.Map(InscripcionDto, Inscripcion);

                var result = await _InscripcionRepository.UpdateInscripcionAsync(Inscripcion);
                if (result)
                {
                    response.Data = InscripcionDto;
                    response.IsSuccess = true;
                    response.Message = "Inscripcion actualizado exitosamente.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo actualizar el Inscripcion.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";
            }

            return response;
        }

        // Eliminar un Inscripcion
        public async Task<Response<InscripcionDTO>> DeleteInscripcionAsync(int id)
        {
            var response = new Response<InscripcionDTO>();
            try
            {
                var Inscripcion = await _InscripcionRepository.GetInscripcionByIdAsync(id);
                if (Inscripcion == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Inscripcion no encontrado.";
                    return response;
                }

                var result = await _InscripcionRepository.DeleteInscripcionAsync(id);
                if (result)
                {
                    response.IsSuccess = true;
                    response.Message = "Inscripcion eliminado exitosamente.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo eliminar el Inscripcion.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";
            }

            return response;
        }

        //Obtener la cantidad de materias inscritas por un estudiante específico
        public async Task<Response<int>> GetMateriasInscritasPorEstudiante(int idEstudiante)
        {
            var response = new Response<int>();
            try
            {
                var result = await _InscripcionRepository.GetMateriasInscritasPorEstudiante(idEstudiante);
                response.Data = result;
                response.IsSuccess = true;
                response.Message = $"El estudiante con ID {idEstudiante} tiene {result} materias inscritas.";
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";
            }
            return response;
        }

        //Verificar si un estudiante tiene otras materias con el mismo profesor
        public async Task<Response<int>> GetMateriasConMismoProfesor(int idEstudiante, int idMateria)
        {
            var response = new Response<int>();
            try
            {
                var result = await _InscripcionRepository.GetMateriasConMismoProfesor(idEstudiante, idMateria);
                response.Data = result;
                response.IsSuccess = true;
                response.Message = $"El estudiante con ID {idEstudiante} tiene {result} materias más con el mismo profesor.";
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";
            }
            return response;
        }

        //Listar los compañeros de clase en las materias del estudiante
        public async Task<Response<List<object>>> GetCompanerosPorMateria(List<int> idMaterias)
        {
            var response = new Response<List<object>>();
            try
            {
                var result = await _InscripcionRepository.GetCompanerosPorMateria(idMaterias);
                response.Data = result;
                response.IsSuccess = result.Count > 0;
                response.Message = result.Count > 0
                    ? "Se encontraron compañeros en las materias inscritas."
                    : "No se encontraron compañeros en las materias inscritas.";
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";
            }
            return response;
        }

    }
}

