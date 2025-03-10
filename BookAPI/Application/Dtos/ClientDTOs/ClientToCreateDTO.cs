
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.ClientDTOs
{
    public class ClientToCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set; }

    }
}


