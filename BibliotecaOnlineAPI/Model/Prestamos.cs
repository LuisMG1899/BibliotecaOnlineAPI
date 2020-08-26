using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.Model
{
    public class Prestamos
    {
        public int IdPrestamo { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaFinalización { get; set; }

        public bool Vencido { get; set; }

        public int? DiasVencido { get; set; }


    }
}
