
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos.SaleOrderDTOs
{
    public class SaleOrderToCreateDTO
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int BookQuantity { get; set; }
    }
}
