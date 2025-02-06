using Microsoft.EntityFrameworkCore;
using UBack.Aplication.Interfaces;
using UBack.Domain.Dominio.Entities;
using UBack.Infraestructure.Persistence.Contexts;

namespace UBack.Infraestructure.Infraestructura
{

    public class RolRepository(UniversidadDbContext context) : IRolRepository
    {
        private readonly UniversidadDbContext _context = context;

        public async Task<bool> AddRolAsync(Rol Rol)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                await _context.Roles.AddAsync(Rol);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteRolAsync(int id)
        {
            try
            {
                var Rol = await _context.Roles.FindAsync(id);
                if (Rol == null)
                    return false;



                // Marcar la entidad como modificada
                _context.Roles.Remove(Rol);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error si es necesario
                Console.WriteLine($"Error al desactivar el Rol: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<Rol>> GetAllRolsAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Rol> GetRolByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }


        public async Task<bool> UpdateRolAsync(Rol Rol)
        {
            try
            {
                var existingRol = await _context.Roles.FindAsync(Rol.Id);
                if (existingRol == null)
                    return false;

                _context.Entry(existingRol).CurrentValues.SetValues(Rol);
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