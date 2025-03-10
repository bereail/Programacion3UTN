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

        public bool IsActive { get; set; } = true;


        public AdminDTO() { }
    }
}
