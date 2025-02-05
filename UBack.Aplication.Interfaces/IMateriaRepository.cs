using UBack.Domain.Dominio.Entities;

namespace UBack.Aplication.Interfaces
{
    public interface IMateriaRepository
    {
        Task<Materia> GetMateriaByIdAsync(int id);
        Task<IEnumerable<Materia>> GetAllMateriasAsync();
        Task<bool> AddMateriaAsync(Materia Materia);
        Task<bool> UpdateMateriaAsync(Materia Materia);
        Task<bool> DeleteMateriaAsync(int id);
    }
}
