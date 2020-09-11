using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BibliotecaOnlineAPI.DTO;
using BibliotecaOnlineAPI.Model;
using BibliotecaOnlineAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaOnlineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //[ApiExplorerSettings(GroupName = "APIBibliotecaLibros") ]
    public class LibrosController : ControllerBase
    {
        private readonly ILibrosRepository _LibroRepo;
        private readonly IMapper _Mapper;
        public LibrosController(ILibrosRepository LibroRepo, IMapper Mapper)
        {
            _LibroRepo = LibroRepo;
            _Mapper = Mapper;
        }


        [HttpGet]
        public IActionResult GetLibros()
        {
            var Lst = _LibroRepo.GetLibro();
            var LstCategoriaDto = new List<LibrosDTO>();
            foreach (var lst in Lst)
            {
                LstCategoriaDto.Add(_Mapper.Map<LibrosDTO>(lst));
            }
            return Ok(LstCategoriaDto);
        }



        [HttpGet("{Id:int}", Name = "GetLibro")]
        public IActionResult GetLibros(int Id)
        {
            var datosLibro = _LibroRepo.GetLibro(Id);
            if (datosLibro == null)
            {
                return NotFound();
            }
            var librosDto = _Mapper.Map<LibrosDTO>(datosLibro);
            return Ok(librosDto);

        }


        [HttpPost]
        public IActionResult CreateLibro([FromBody] LibrosDTO libroDto)
        {
            if (libroDto == null)
            {
                return BadRequest(ModelState);
            }
            else if (_LibroRepo.ExisteLibro(libroDto.Nombre))
            {
                ModelState.AddModelError("", "El libro ya existe");

                return StatusCode(404, ModelState);
            }

            var libro = _Mapper.Map<Libros>(libroDto);

            int idLibro = _LibroRepo.CreateLibro(libro);

            if (idLibro == 0)
            {
                ModelState.AddModelError("", "El libro" + libroDto.Nombre + "no se pudo crear");
                return StatusCode(500, ModelState);

            }

            return Ok(idLibro);
        }



        [HttpPatch("{Id:int}", Name = "UpdateLibro")]
        public IActionResult UpdateLibro(int Id, [FromBody] LibrosDTO libroDto)
        {
            if (libroDto == null) { return BadRequest(ModelState); }
            var Libro = _Mapper.Map<Libros>(libroDto);
            var item = _LibroRepo.UpdateLibro(Libro);

            if (item == null)
            {
                ModelState.AddModelError("", "La categoria no se pudo actualizar");
                return StatusCode(500, ModelState);
            }

            return Ok(Libro);

        }





        [HttpDelete("{Id:int}", Name = "DeleteLibro")]

        public IActionResult DeleteLibro(int Id) {
            if (!_LibroRepo.ExisteLibro(Id)) { return NotFound(); }
            if (!_LibroRepo.DeleteLibro(Id))
            {
                ModelState.AddModelError("", "La categoria no se pudo borrar");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }


}