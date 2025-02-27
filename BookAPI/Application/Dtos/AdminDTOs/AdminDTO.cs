namespace Application.Dtos.AdminDTOs
{
    public class AdminDTO
    {
        public int AdminId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public AdminDTO(int id, string email)
        {
            AdminId = id;
            Email = email;
        }

        // Constructor sin parámetros por si lo necesitas en mapeo automático
        public AdminDTO() { }
    }
}
