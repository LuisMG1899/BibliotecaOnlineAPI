using BibliotecaOnlineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.Repository.IRepository
{
   public interface ILibrosRepository
    {
        int CreateLibro(Libros DatosLibro);
        ICollection<int> CreateLibro(ICollection<Libros> DatosLibro);
        bool ExisteLibro(string Nombre);
        bool ExisteLibro(int id);
        ICollection<Libros> GetLibro();
        Libros GetLibro(int id);
        bool DeleteLibro(int id);
        Libros UpdateLibro(Libros DatosLibro);
        ICollection<Libros> UpdateLibro(ICollection<Libros> DatosLibro);
    }
}
