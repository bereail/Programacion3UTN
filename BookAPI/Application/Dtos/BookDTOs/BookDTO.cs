using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.Dtos.BookDTOs
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookGenre Genre { get; set; }

        public float Price { get; set; }
        public int Stock { get; set; }

        public string Description { get; set; }
    }
}
