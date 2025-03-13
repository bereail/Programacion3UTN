using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos.BookDTOs
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }

        //Convierte el enum en text
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookGenre Genre { get; set; }
        [Required]
        public float Price { get; set; }
        public string Description { get; set; }

        [Required]
        public int Stock { get; set; }

    }
}
