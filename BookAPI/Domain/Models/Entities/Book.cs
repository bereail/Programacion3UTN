using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public BookGenre Genre { get; set; }

        [Required]
        public string Author { get; set; } = string.Empty;

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public string? Description { get; set; }

        public ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
    }
}
