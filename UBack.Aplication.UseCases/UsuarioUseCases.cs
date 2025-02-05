using AutoMapper;
using UBack.Aplication.Dtos;
using UBack.Aplication.Interfaces;
using UBack.Domain.Dominio.Entities;
using UBack.Transversal.Common;

namespace UBack.Aplication.UseCases
{
    public class UsuarioUseCases(IUsuarioRepository UsuarioRepository, IMapper mapper)
    {
        private readonly IUsuarioRepository _UsuarioRepository = UsuarioRepository;
        private readonly IMapper _mapper = mapper;

        // Agregar un nuevo usuario
        public async Task<Response<UsuarioDTO>> AddUsuarioAsync(UsuarioDTO UsuarioDto)
        {
            var response = new Response<UsuarioDTO>();
            try
            {

                // Mapeo del DTO a modelo de dominio
                var usuario = _mapper.Map<Usuario>(UsuarioDto);                

                // Agregar el usuario en la base de datos
                var result = await _UsuarioRepository.AddUsuarioAsync(usuario);
                if (result)
                {
                    response.Data = UsuarioDto;
                    response.IsSuccess = true;
                    response.Message = "Usuario creado exitosamente.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo crear el usuario.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";
            }

            return response;
        }

        // Obtener un usuario por ID
        public async Task<UsuarioDTO> GetUsuarioByIdAsync(int id)
        {
            var Usuario = await _UsuarioRepository.GetUsuarioByIdAsync(id);
            return Usuario != null ? _mapper.Map<UsuarioDTO>(Usuario) : null;
        }

        // Obtener todos los usuarios
        public async Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync()
        {
            var Usuarios = await _UsuarioRepository.GetAllUsuariosAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(Usuarios);
        }

        // Actualizar un usuario
        public async Task<Response<UsuarioDTO>> UpdateUsuarioAsync(int id, UsuarioDTO UsuarioDto)
        {
            var response = new Response<UsuarioDTO>();
            try
            {
                var Usuario = await _UsuarioRepository.GetUsuarioByIdAsync(id);
                if (Usuario == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Usuario no encontrado.";
                    return response;
                }

                // Mapeo del DTO al modelo de dominio
                _mapper.Map(UsuarioDto, Usuario);

                var result = await _UsuarioRepository.UpdateUsuarioAsync(Usuario);
                if (result)
                {
                    response.Data = UsuarioDto;
                    response.IsSuccess = true;
                    response.Message = "Usuario actualizado exitosamente.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo actualizar el usuario.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";
            }

            return response;
        }

        // Eliminar un usuario
        public async Task<Response<UsuarioDTO>> DeleteUsuarioAsync(int id)
        {
            var response = new Response<UsuarioDTO>();
            try
            {
                var Usuario = await _UsuarioRepository.GetUsuarioByIdAsync(id);
                if (Usuario == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Usuario no encontrado.";
                    return response;
                }

                var result = await _UsuarioRepository.DeleteUsuarioAsync(id);
                if (result)
                {
                    response.IsSuccess = true;
                    response.Message = "Usuario eliminado exitosamente.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo eliminar el usuario.";
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
