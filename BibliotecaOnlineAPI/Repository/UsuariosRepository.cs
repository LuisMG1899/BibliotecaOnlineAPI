using BibliotecaOnlineAPI.Infraestructure;
using BibliotecaOnlineAPI.Model;
using BibliotecaOnlineAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.Repository
{
    public class UsuariosRepository : IUsuariosRepository 
    {
        private readonly BibliotecaDBContext _dbBib;

        public UsuariosRepository(BibliotecaDBContext dbBib) {
            _dbBib = dbBib;
        }
        public int CreateUsuario(Usuarios DatosUsuario)
        {
            _dbBib.Usuarios.Add(DatosUsuario);
            _dbBib.SaveChanges();
            return DatosUsuario.IdUsuario;
           
        }

        public bool DeleteUsuario(int id)
        {
            var datoUsuario = _dbBib.Usuarios.Where(x => x.IdUsuario == id).FirstOrDefault();
            _dbBib.Usuarios.Remove(datoUsuario);
            _dbBib.SaveChanges();
            return true;
        }

        public bool ExisteUsuario(string Correo)
        {
           var datoUsuario = _dbBib.Usuarios.Any(x => x.Correo == Correo);
            if (datoUsuario == false)
            {
                return false;
            }
            else {
                return true;
            }
           

        }

        public bool ExisteUsuario(int id)
        {
            var datoUsuario = _dbBib.Usuarios.Where(x => x.IdUsuario == id).FirstOrDefault();
            if (datoUsuario == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public ICollection<Usuarios> GetUsuario()
        {
            var datosUsuario = _dbBib.Usuarios.ToList();

            return datosUsuario;
        }

        public Usuarios GetUsuarios(int Id)
        {
            var datoUsuario = _dbBib.Usuarios.FirstOrDefault(x => x.IdUsuario == Id);
            return datoUsuario;

        }

        public Usuarios Login(string correo, string Password)
        {
            var usuarioCredencial = _dbBib.Usuarios.Where(x => x.Correo == correo).FirstOrDefault();
            if (usuarioCredencial == null) {
                return null;
            }
            if (!Criptography.ValidacionPassword(Password, usuarioCredencial.HashPassword, usuarioCredencial.SaltPass))
            {
                return null;
            }
            return usuarioCredencial;
        }

        public int Registrar(Usuarios usuario, string Password)
        {
        
            byte[] HassPassword, SaltPassword;
            Criptography.CrearPasswordEncriptado(Password, out HassPassword, out SaltPassword);
            usuario.HashPassword = HassPassword;
            usuario.SaltPass = SaltPassword;
            _dbBib.Usuarios.Add(usuario);
            _dbBib.SaveChanges();
            return usuario.IdUsuario;
        }

        public Usuarios UpdateUsuario(Usuarios DatosUsuario)
        {
            var datosUsuario = _dbBib.Usuarios.Where(x => x.IdUsuario == DatosUsuario.IdUsuario).FirstOrDefault();

            datosUsuario.Nombres = DatosUsuario.Nombres;

            _dbBib.SaveChanges();

            return datosUsuario;
        }
    }
}
