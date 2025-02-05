using UBack.Domain.Dominio.Entities;

namespace UBack.Aplication.Interfaces
{

    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
        Task<bool> AddUsuarioAsync(Usuario Usuario);
        Task<bool> UpdateUsuarioAsync(Usuario Usuario);
        Task<bool> DeleteUsuarioAsync(int id);
    }
}
