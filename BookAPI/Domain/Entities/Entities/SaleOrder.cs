using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using System.Security.Cryptography.X509Certificates;
using Domain.Enums;

namespace Domain.Entities.Entities
{
    public class SaleOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleOrderId { get; set; }
        public Client Client { get; set; }
        [Required]
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        public Book Book { get; set; }
        [Required]
        [ForeignKey("BookId")]
        public int BookId { get; set; }
        public int BookQuantity { get; set; }

        [Required]
        public SaleOrderStatus Status { get; set; }

        public float Total
        {
            get
            {
                return (Book != null) ? BookQuantity * Book.Price : 0;
            }
        }

    }
}
