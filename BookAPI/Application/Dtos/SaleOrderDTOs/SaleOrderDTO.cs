using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.Dtos.SaleOrderDTOs
{
    public class SaleOrderDTO
    {
        public int SaleOrderId { get; set; }
        public int ClientId { get; set; }
        public int BookId { get; set; }
        public int BookQuantity { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SaleOrderStatus Status { get; set; }
        public float Total { get; set; }
    }
}
