using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBack.Aplication.Dtos
{
    public class AuthResponseDTO
    {
        public string? Token { get; set; }
        public string? Name { get; set; }
        public int Rol { get; set; }
        public  int Id { get; set; }
        public DateTime Expiration { get; set; }
    }
}
