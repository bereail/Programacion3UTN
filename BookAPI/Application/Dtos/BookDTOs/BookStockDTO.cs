using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos.BookDTOs
{
    public class BookStockDTO
    {
        [Required]
        public int Stock { get; set; }
    }
}
