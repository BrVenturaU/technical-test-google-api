using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataTransferObjects.User
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "La nueva contraseña es requerida.")]
        [Compare("PasswordConfirmation", ErrorMessage = "Las contraseñas deben ser iguales.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "La confirmación de la nueva contraseña es requerida.")]
        [Compare("Password", ErrorMessage = "Las contraseñas deben ser iguales.")]
        public string PasswordConfirmation { get; set; }
    }
}
