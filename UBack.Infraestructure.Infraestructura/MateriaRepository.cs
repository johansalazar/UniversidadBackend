using Microsoft.EntityFrameworkCore;
using UBack.Aplication.Interfaces;
using UBack.Domain.Dominio.Entities;
using UBack.Infraestructure.Persistence.Contexts;

namespace UBack.Infraestructure.Infraestructura
{

    public class MateriaRepository(UniversidadDbContext context) : IMateriaRepository
    {
        private readonly UniversidadDbContext _context = context;

        public async Task<bool> AddMateriaAsync(Materia Materia)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                await _context.Materias.AddAsync(Materia);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteMateriaAsync(int id)
        {
            try
            {
                var Materia = await _context.Materias.FindAsync(id);
                if (Materia == null)
                    return false;



                // Marcar la entidad como modificada
                _context.Materias.Remove(Materia);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error si es necesario
                Console.WriteLine($"Error al desactivar el Materia: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<Materia>> GetAllMateriasAsync()
        {
            return await _context.Materias.ToListAsync();
        }

        public async Task<Materia> GetMateriaByIdAsync(int id)
        {
            return await _context.Materias.FindAsync(id);
        }


        public async Task<bool> UpdateMateriaAsync(Materia Materia)
        {
            try
            {
                var existingMateria = await _context.Materias.FindAsync(Materia.Id);
                if (existingMateria == null)
                    return false;

                _context.Entry(existingMateria).CurrentValues.SetValues(Materia);
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
