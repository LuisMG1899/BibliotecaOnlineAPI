using AutoMapper;
using BibliotecaOnlineAPI.DTO;
using BibliotecaOnlineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.Mapper
{
    public class LibrosMapper : Profile
    {
        public LibrosMapper()
        {
            CreateMap<Libros, LibrosDTO>().ReverseMap();
            CreateMap<Usuarios, UsuariosDTO>().ReverseMap();
        }
    }
}
