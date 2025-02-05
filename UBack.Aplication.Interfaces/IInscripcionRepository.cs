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
    }
}
