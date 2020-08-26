using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.Model
{
    public class Direcciones
    {
        public int IdDireccion { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Municipio { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }

        public string EntreCalle1 { get; set; }
        public string EntreCalle2 { get; set; }

        public string Numero { get; set; }
    }
}
