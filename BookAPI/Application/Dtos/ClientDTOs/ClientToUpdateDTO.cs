using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.ClientDTOs
{
    public class ClientToUpdateDTO
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
