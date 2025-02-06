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
                _context.Inscripciones.Remove(Inscripcion);

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

        // Obtener la cantidad de materias inscritas por un estudiante específico
        public async Task<int> GetMateriasInscritasPorEstudiante(int idEstudiante)
        {
            return await _context.Inscripciones
                .Where(i => i.IdEstudiante == idEstudiante)
                .CountAsync();
        }

        // Verificar si un estudiante tiene otras materias con el mismo profesor
        public async Task<int> GetMateriasConMismoProfesor(int idEstudiante, int idMateria)
        {
            var profesorId = await _context.Materias
                .Where(m => m.Id == idMateria)
                .Select(m => m.IdProfesor)
                .FirstOrDefaultAsync();

            if (profesorId == 0)
                return 0; // No hay profesor asignado o la materia no existe.

            return await _context.Inscripciones
                .Join(_context.Materias,
                    ins => ins.IdMateria,
                    mat => mat.Id,
                    (ins, mat) => new { ins.IdEstudiante, mat.IdProfesor, mat.Id })
                .Where(g => g.IdEstudiante == idEstudiante && g.IdProfesor == profesorId && g.Id != idMateria)
                .CountAsync();
        }

        //Listar los compañeros de clase en las materias del estudiante
        public async Task<List<object>> GetCompanerosPorMateria(List<int> idMaterias)
        {
            return await _context.Inscripciones
                .Join(_context.Usuarios,
                    ins => ins.IdEstudiante,
                    usr => usr.Id,
                    (ins, usr) => new { ins.IdMateria, Estudiante = usr })
                .Where(g => idMaterias.Contains(g.IdMateria))
                .Join(_context.Inscripciones,
                    i1 => i1.IdMateria,
                    i2 => i2.IdMateria,
                    (i1, i2) => new { i1.IdMateria, i1.Estudiante, i2.IdEstudiante })
                .Join(_context.Usuarios,
                    i3 => i3.IdEstudiante,
                    usr2 => usr2.Id,
                    (i3, usr2) => new { i3.IdMateria, i3.Estudiante, Compañero = usr2 })
                .Where(g => g.Estudiante.Id != g.Compañero.Id)
                .Select(g => new
                {
                    IdMateria = g.IdMateria,
                    NombreEstudiante = g.Estudiante.Nombre,
                    NombreCompañero = g.Compañero.Nombre
                })
                .OrderBy(g => g.IdMateria)
                .ToListAsync<object>();
        }
    }
}

