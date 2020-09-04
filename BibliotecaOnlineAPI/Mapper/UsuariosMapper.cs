using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BibliotecaOnlineAPI.DTO;
using BibliotecaOnlineAPI.Model;

namespace BibliotecaOnlineAPI.Mapper
{
    public class UsuariosMapper :Profile
    {
        public UsuariosMapper()
        {
            CreateMap<Usuarios, UsuariosDTO>().ReverseMap();

        }
    }
}
