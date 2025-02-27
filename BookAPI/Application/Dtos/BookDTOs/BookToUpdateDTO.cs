using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos.BookDTOs
{
    public class BookToUpdateDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookGenre Genre { get; set; }

        public float Price { get; set; }
        public int Stock { get; set; }

        public string Description { get; set; }
    }
}
