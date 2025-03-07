using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos.BookDTOs
{
    public class BookToCreateDTO
    {

        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        public BookGenre Genre { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        public float Price { get; set; }
        public string Description { get; set; }
       
        public int Stock { get; set; }
    }
}
