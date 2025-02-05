using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBack.Domain.Dominio.Entities
{
    public class Rol
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; } // "Estudiante" o "Profesor"
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
