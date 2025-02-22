using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.Dtos.SaleOrderDTOs
{
    public class SaleOrderStatusDTO
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SaleOrderStatus Status { get; set; }
    }
}
