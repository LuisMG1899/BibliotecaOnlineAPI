using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.DTO
{
    public class UsuarioAuthDTO
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "El nombre de usuario correo es necesario")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength (16, MinimumLength = 6, ErrorMessage = "La contraseña debe ser entre 6 y 12 caracteres")]
        public string Password { get; set; }
    }
}
