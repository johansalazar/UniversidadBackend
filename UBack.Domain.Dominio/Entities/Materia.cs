using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBack.Domain.Dominio.Entities
{
    public class Materia
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Creditos { get; set; } = 3;
        public int? IdProfesor { get; set; }
        public Usuario? Profesor { get; set; }
    }
}
