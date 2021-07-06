using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataTransferObjects.User
{
    public class UserForCreationDto
    {
        [Required(ErrorMessage = "El primer nombre es requerido.")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es requerido.")] 
        public string UserName { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida.")] 
        [Compare("PasswordConfirmation", ErrorMessage = "Las contraseñas deben ser iguales.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "La confirmación de la contraseña es requerida.")]
        [Compare("Password", ErrorMessage = "Las contraseñas deben ser iguales.")]
        public string PasswordConfirmation { get; set; }
        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        public string Email { get; set; }
    }
}
