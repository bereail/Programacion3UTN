
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.ClientDTOs
{
    public class ClientDTO
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public bool IsActive { get; set; } = true;
        public ClientDTO(int clientId, string email)
        {
            Email = email;
        }

        public ClientDTO() { }
    }
}
