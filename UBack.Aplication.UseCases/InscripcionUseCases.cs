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
    }
}

