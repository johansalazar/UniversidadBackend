using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBack.Aplication.Dtos
{
    public class RolDTO
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; } // "Administrador" o "Estudiante" o "Profesor"
       
    }
}
