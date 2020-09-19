using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using BibliotecaOnlineAPI.DTO;
using BibliotecaOnlineAPI.Model;
using BibliotecaOnlineAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BibliotecaOnlineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "APIBibliotecaUsuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _UsuariosRepo;
        private readonly IMapper _Mapper;
        private readonly IConfiguration _config;
        public UsuariosController(IUsuariosRepository UsuariosRepo, IMapper Mapper, IConfiguration Config)
        {
            _UsuariosRepo = UsuariosRepo;
            _Mapper = Mapper;
            _config = Config;
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(List<UsuariosDTO>))]
        [ProducesResponseType(400, Type = typeof(List<UsuariosDTO>))]
        public IActionResult GetUsuarios()
        {
            var Lst = _UsuariosRepo.GetUsuario();
            var LstUsuarioDTO = new List<UsuariosDTO>();
            foreach (var lst in Lst)
            {
                LstUsuarioDTO.Add(_Mapper.Map<UsuariosDTO>(lst));
            }
            return Ok(LstUsuarioDTO);
        }

        /// <summary>
        /// jejejejej
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{Id:int}", Name = "GetUsuario")]
        public IActionResult GetUsuarios(int Id)
        {
            var datos = _UsuariosRepo.GetUsuarios(Id);
            if (datos == null)
            {
                return NotFound();
            }
            var usuarioDto = _Mapper.Map<UsuariosDTO>(datos);
            return Ok(usuarioDto);

        }


        [HttpPost]
        public IActionResult CreateUsuario([FromBody] UsuariosDTO usuariosDTO)
        {
            if (usuariosDTO == null)
            {
                return BadRequest(ModelState);
            }
            else if (_UsuariosRepo.ExisteUsuario(usuariosDTO.Correo))
            {
                ModelState.AddModelError("", "El usuario ya existe");

                return StatusCode(404, ModelState);
            }

            var usuario = _Mapper.Map<Usuarios>(usuariosDTO);

            int idUser = _UsuariosRepo.CreateUsuario(usuario);

            if (idUser == 0)
            {
                ModelState.AddModelError("", "El usuario" + usuariosDTO.Correo + "no se pudo crear");
                return StatusCode(500, ModelState);

            }

            return Ok(idUser);
        }


        [HttpPatch("{Id:int}", Name = "UpdateUsuario")]
        public IActionResult UpdateUsuario(int Id, [FromBody] UsuariosDTO usuarioDto)
        {
            if (usuarioDto == null) { return BadRequest(ModelState); }
            var Usuario = _Mapper.Map<Usuarios>(usuarioDto);
            var item = _UsuariosRepo.UpdateUsuario(Usuario);

            if (item == null)
            {
                ModelState.AddModelError("", "El ususario no se pudo actualizar");
                return StatusCode(500, ModelState);
            }

            return Ok(Usuario);

        }


        [HttpDelete("{Id:int}", Name = "DeleteUsuario")]

        public IActionResult DeleteUsuario(int Id)
        {
            if (!_UsuariosRepo.ExisteUsuario(Id)) { return NotFound(); }
            if (!_UsuariosRepo.DeleteUsuario(Id))
            {
                ModelState.AddModelError("", "El usuario no se pudo borrar");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPost("Registrar")]
        [ProducesResponseType(201, Type = typeof(int)) ]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Registrar(UsuarioAuthDTO DatosRegistro)
        {
            if (_UsuariosRepo.ExisteUsuario(DatosRegistro.Correo.ToLower())) {
                return BadRequest("El usuario " + DatosRegistro.Correo + "ya existe");
            }
            var CreUsuario = new Usuarios
            {
                Correo = DatosRegistro.Correo,
            };
            var result = _UsuariosRepo.Registrar(CreUsuario, DatosRegistro.Password);

            return Ok(result);
        }

        [HttpPost("Login")]
        public ActionResult Login(UsuarioAuthLoginDTO datosRegistro) {
            var usuarioCredencial = _UsuariosRepo.Login(datosRegistro.Correo, datosRegistro.Password);

            if (usuarioCredencial == null) {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,usuarioCredencial.Correo.ToString()),
                new Claim(ClaimTypes.Name, usuarioCredencial.Correo), 
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:TokenKey").Value));
            var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var descriptorToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credencial,
            };

            var tokenHandle = new JwtSecurityTokenHandler();
            var token = tokenHandle.CreateToken(descriptorToken);
            return Ok(new
            {
                token = tokenHandle.WriteToken(token)
            }) ;
        }
    }
}