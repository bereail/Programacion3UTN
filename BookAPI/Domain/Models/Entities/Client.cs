using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Client : User
    {
        public ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
    }
}
