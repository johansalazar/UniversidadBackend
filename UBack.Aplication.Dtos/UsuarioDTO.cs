﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBack.Aplication.Dtos
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Contrasena { get; set; }
        public int IdRol { get; set; }
      //  public RolDTO? Rol { get; set; }
    }
}
