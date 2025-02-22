using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.AdminDTOs
{
    public class AdminToUpdateDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
