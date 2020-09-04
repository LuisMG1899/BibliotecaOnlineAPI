using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.DTO
{
    public class UsuarioAuthLoginDTO
    {
        [Required]
        public string Correo { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
