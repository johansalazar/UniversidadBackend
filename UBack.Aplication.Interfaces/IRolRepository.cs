using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBack.Domain.Dominio.Entities;

namespace UBack.Aplication.Interfaces
{
    public interface IRolRepository
    {
        Task<Rol> GetRolByIdAsync(int id);
        Task<IEnumerable<Rol>> GetAllRolsAsync();
        Task<bool> AddRolAsync(Rol Rol);
        Task<bool> UpdateRolAsync(Rol Rol);
        Task<bool> DeleteRolAsync(int id);
    }
}
