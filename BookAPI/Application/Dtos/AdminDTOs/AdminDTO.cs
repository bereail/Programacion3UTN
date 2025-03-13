namespace Application.Dtos.AdminDTOs
{
    public class AdminDTO
    {
        public int AdminId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }


        //Constructor con parámetros -> Asegurar que el objeto siempre tenga valores válidos
        public AdminDTO(int id, string email)
        {
            AdminId = id;
            Email = email;
        }

        public bool IsActive { get; set; } = true;


        /*Constructor vacío → Permite crear instancias de AdminDTO sin pasar parámetros.*/
        public AdminDTO() { }
    }
}
