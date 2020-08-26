using BibliotecaOnlineAPI.Infraestructure;
using BibliotecaOnlineAPI.Model;
using BibliotecaOnlineAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.Repository
{
    
    public class LibrosRepository : ILibrosRepository

    {
        private readonly BibliotecaDBContext _bdBib;

        public LibrosRepository(BibliotecaDBContext bdBib)
        {
            _bdBib = bdBib;
        }

        public int CreateLibro(Libros DatosLibro) {
            _bdBib.Libros.Add(DatosLibro);
            _bdBib.SaveChanges();

            return DatosLibro.IdLibro;
        }

        public  ICollection<int> CreateLibro(ICollection<Libros> DatosLibro)
        {
            _bdBib.Libros.AddRange(DatosLibro);
            _bdBib.SaveChanges();

            return DatosLibro.Select(x => x.IdLibro).ToList();
        }


        public bool ExisteLibro(string Nombre)
        {
            return _bdBib.Libros.Any(x => x.Nombre == Nombre);
        }


        public Libros GetLibro(int id)
        {
            var itemLibro = _bdBib.Libros.Where(x => x.IdLibro == id).FirstOrDefault();

            return itemLibro;
        }

        public bool DeleteLibro(int id)
        {
            var itemLibro = _bdBib.Libros.Where(x => x.IdLibro == id).FirstOrDefault();

            _bdBib.Libros.Remove(itemLibro);
            _bdBib.SaveChanges();
            return true;
        }

        public Libros UpdateLibro(Libros DatosLibro)
        {
            var itemLibro = _bdBib.Libros.Where(x => x.IdLibro == DatosLibro.IdLibro).FirstOrDefault();
            itemLibro.Nombre = DatosLibro.Nombre;
            _bdBib.SaveChanges();
            return DatosLibro;
        }

        public ICollection<Libros> UpdateLibro(ICollection<Libros> DatosLibro)
        {
            var ListItem = _bdBib.Libros.Where(x => DatosLibro.Select(i => i.IdLibro).ToList().Contains(x.IdLibro)).ToList();
                ListItem.ForEach(u =>
                {
                    u.Nombre = DatosLibro.Where(x => x.IdLibro == u.IdLibro).Select(n => n.Nombre).FirstOrDefault();
                });
            _bdBib.SaveChanges();

            return DatosLibro;
        }

        public bool ExisteLibro(int id)
        {
            var libro = _bdBib.Libros.Where(x => x.Id == id);
            if (libro == null) {
                return false;
            }
            return true;
        }

        public ICollection<Libros> GetLibro()
        {
            var listaLibros = _bdBib.Libros.ToList();

            return listaLibros;
        }
    }
}
