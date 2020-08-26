﻿using BibliotecaOnlineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.Repository.IRepository
{
   public interface IUsuariosRepository
    {
        ICollection<Usuarios> GetUsuario();
        Usuarios GetUsuarios(int Id);
        bool ExisteUsuario(string Nombre);
        bool ExisteUsuario(int id);
        int CreateUsuario(Usuarios DatosUsuario);
        bool DeleteUsuario(int id);

        Usuarios UpdateUsuario(Usuarios DatosUsuario);
    }
}
