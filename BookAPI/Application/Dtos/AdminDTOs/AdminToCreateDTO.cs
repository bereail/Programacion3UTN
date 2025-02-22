using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.AdminDTOs
{
    public class AdminToCreateDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
