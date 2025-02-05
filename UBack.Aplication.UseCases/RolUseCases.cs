using AutoMapper;
using UBack.Aplication.Dtos;
using UBack.Aplication.Interfaces;
using UBack.Domain.Dominio.Entities;
using UBack.Transversal.Common;

namespace UBack.Aplication.UseCases
{
    public class RolUseCases(IRolRepository RolRepository, IMapper mapper)
    {
        private readonly IRolRepository _RolRepository = RolRepository;
        private readonly IMapper _mapper = mapper;

        // Agregar un nuevo Rol
        public async Task<Response<RolDTO>> AddRolAsync(RolDTO RolDto)
        {
            var response = new Response<RolDTO>();
            try
            {

                // Mapeo del DTO a modelo de dominio
                var Rol = _mapper.Map<Rol>(RolDto);

                // Agregar el Rol en la base de datos
                var result = await _RolRepository.AddRolAsync(Rol);
                if (result)
                {
                    response.Data = RolDto;
                    response.IsSuccess = true;
                    response.Message = "Rol creado exitosamente.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo crear el Rol.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";
            }

            return response;
        }

        // Obtener un Rol por ID
        public async Task<RolDTO> GetRolByIdAsync(int id)
        {
            var Rol = await _RolRepository.GetRolByIdAsync(id);
            return Rol != null ? _mapper.Map<RolDTO>(Rol) : null;
        }

        // Obtener todos los Rols
        public async Task<IEnumerable<RolDTO>> GetAllRolsAsync()
        {
            var Rols = await _RolRepository.GetAllRolsAsync();
            return _mapper.Map<IEnumerable<RolDTO>>(Rols);
        }

        // Actualizar un Rol
        public async Task<Response<RolDTO>> UpdateRolAsync(int id, RolDTO RolDto)
        {
            var response = new Response<RolDTO>();
            try
            {
                var Rol = await _RolRepository.GetRolByIdAsync(id);
                if (Rol == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Rol no encontrado.";
                    return response;
                }

                // Mapeo del DTO al modelo de dominio
                _mapper.Map(RolDto, Rol);

                var result = await _RolRepository.UpdateRolAsync(Rol);
                if (result)
                {
                    response.Data = RolDto;
                    response.IsSuccess = true;
                    response.Message = "Rol actualizado exitosamente.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo actualizar el Rol.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";
            }

            return response;
        }

        // Eliminar un Rol
        public async Task<Response<RolDTO>> DeleteRolAsync(int id)
        {
            var response = new Response<RolDTO>();
            try
            {
                var Rol = await _RolRepository.GetRolByIdAsync(id);
                if (Rol == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Rol no encontrado.";
                    return response;
                }

                var result = await _RolRepository.DeleteRolAsync(id);
                if (result)
                {
                    response.IsSuccess = true;
                    response.Message = "Rol eliminado exitosamente.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo eliminar el Rol.";
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