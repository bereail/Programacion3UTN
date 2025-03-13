using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;


namespace Domain.Entities.Entities
{
    //Padre de client y admin 
    //Clase abstracta: no se puede instanciar, solo heredar de ella (no me interesa que se cree un usuario sin Role)
    public abstract class User
    {

        [Key]       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(20)]
        public string? Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string? UserName { get; set; }

        [Required]
        [Column("Role")]
        [EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
