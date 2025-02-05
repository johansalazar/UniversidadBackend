using Microsoft.EntityFrameworkCore;
using UBack.Aplication.Interfaces;
using UBack.Domain.Dominio.Entities;
using UBack.Infraestructure.Persistence.Contexts;

namespace UBack.Infraestructure.Infraestructura
{
    public class UsuarioRepository(UniversidadDbContext context) : IUsuarioRepository
    {
        private readonly UniversidadDbContext _context = context;

        public async Task<bool> AddUsuarioAsync(Usuario usuario)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                    return false;



                // Marcar la entidad como modificada
                _context.Usuarios.Update(usuario);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error si es necesario
                Console.WriteLine($"Error al desactivar el usuario: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }


        public async Task<bool> UpdateUsuarioAsync(Usuario usuario)
        {
            try
            {
                var existingusuario = await _context.Usuarios.FindAsync(usuario.Id);
                if (existingusuario == null)
                    return false;

                _context.Entry(existingusuario).CurrentValues.SetValues(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
