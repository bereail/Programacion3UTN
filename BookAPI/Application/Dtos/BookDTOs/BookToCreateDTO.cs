using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos.BookDTOs
{
    public class BookToCreateDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public BookGenre Genre { get; set; }
        [Required]
        public string Author { get; set; }

        [Required]
        public float Price { get; set; }
        public string Description { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}
