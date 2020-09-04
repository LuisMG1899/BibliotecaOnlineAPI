using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.DTO
{
    public class UsuariosDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        //public Direcciones Direccion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public byte[] HasPassword { get; set; }
    }
}
