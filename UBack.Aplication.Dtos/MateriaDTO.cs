using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBack.Aplication.Dtos
{
    public class MateriaDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Creditos { get; set; } = 3;
        public int? IdProfesor { get; set; }
       // public UsuarioDTO Profesor { get; set; }
    }
}
