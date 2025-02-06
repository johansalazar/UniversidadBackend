using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBack.Domain.Dominio.Entities;

namespace UBack.Aplication.Interfaces
{
   
    public interface IInscripcionRepository
    {
        Task<Inscripcion> GetInscripcionByIdAsync(int id);
        Task<IEnumerable<Inscripcion>> GetAllInscripcionAsync();
        Task<bool> AddInscripcionAsync(Inscripcion Inscripcion);
        Task<bool> UpdateInscripcionAsync(Inscripcion Inscripcion);
        Task<bool> DeleteInscripcionAsync(int id);
        //Obtener la cantidad de materias inscritas por un estudiante específico
        Task<int> GetMateriasInscritasPorEstudiante(int idEstudiante);

        //Verificar si un estudiante tiene otras materias con el mismo profesor
        Task<int> GetMateriasConMismoProfesor(int idEstudiante, int idMateria);

        //Listar los compañeros de clase en las materias del estudiante
        Task<List<object>> GetCompanerosPorMateria(List<int> idMaterias);
       
    }
}
