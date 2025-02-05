using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBack.Aplication.Interfaces;
using UBack.Domain.Dominio.Entities;
using UBack.Infraestructure.Persistence.Contexts;

namespace UBack.Infraestructure.Infraestructura
{
    public class InscripcionRepository(UniversidadDbContext context) : IInscripcionRepository
    {
        private readonly UniversidadDbContext _context = context;

        public async Task<bool> AddInscripcionAsync(Inscripcion Inscripcion)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                await _context.Inscripciones.AddAsync(Inscripcion);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteInscripcionAsync(int id)
        {
            try
            {
                var Inscripcion = await _context.Inscripciones.FindAsync(id);
                if (Inscripcion == null)
                    return false;



                // Marcar la entidad como modificada
                _context.Inscripciones.Update(Inscripcion);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error si es necesario
                Console.WriteLine($"Error al desactivar el Inscripcion: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<Inscripcion>> GetAllInscripcionAsync()
        {
            return await _context.Inscripciones.ToListAsync();
        }

        public async Task<Inscripcion> GetInscripcionByIdAsync(int id)
        {
            return await _context.Inscripciones.FindAsync(id);
        }


        public async Task<bool> UpdateInscripcionAsync(Inscripcion Inscripcion)
        {
            try
            {
                var existingInscripcion = await _context.Inscripciones.FindAsync(Inscripcion.Id);
                if (existingInscripcion == null)
                    return false;

                _context.Entry(existingInscripcion).CurrentValues.SetValues(Inscripcion);
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

