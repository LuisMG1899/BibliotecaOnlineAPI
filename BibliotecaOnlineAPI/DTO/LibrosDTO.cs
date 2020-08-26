using BibliotecaOnlineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.DTO
{
    public class LibrosDTO
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
