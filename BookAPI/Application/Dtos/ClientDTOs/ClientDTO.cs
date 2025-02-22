
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.ClientDTOs
{
    public class ClientDTO
    {
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
