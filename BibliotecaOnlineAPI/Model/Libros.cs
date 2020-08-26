using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.Model
{
    
    public class Libros
    {
     
        public int Id { get; set; }
      
        public string Nombre { get; set; }
      
        public int IdLibro { get; set; }
       
        public bool Activo { get; set; }
       
        public DateTime FechaCreacion { get; set; }

        public int? UsuarioId { get; set; }
        public Usuarios UsuarioLibro { get; set; }

    }
}
