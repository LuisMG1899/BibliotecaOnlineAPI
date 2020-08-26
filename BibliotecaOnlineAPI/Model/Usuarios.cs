using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.Model
{
  
    public class Usuarios
    {
        
        public int IdUsuario { get; set; }
        
      
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        //public Direcciones Direccion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public ICollection<Libros> LibrosUsuario { get; set; }
    }

}
