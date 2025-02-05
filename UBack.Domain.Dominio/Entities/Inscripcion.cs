using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBack.Domain.Dominio.Entities
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public int IdEstudiante { get; set; }
        public Usuario? Estudiante { get; set; }
        public int IdMateria { get; set; }
        public Materia? Materia { get; set; }
    }
}
