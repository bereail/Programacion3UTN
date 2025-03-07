using Application.Dtos.SaleOrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ISaleOrderService
    {
        public SaleOrderDTO? GetSaleOrder(int SaleOrderId);
        public ICollection<SaleOrderDTO> GetAllSaleOrders();
        public SaleOrderDTO? AddSaleOrder(SaleOrderToCreateDTO saleOrderToCreateDTO, int clientId);

        public SaleOrderStatusDTO? UpdateSaleOrderStatus(int saleOrderId);
        public SaleOrderDTO? DeleteSaleOrder(int saleOrderId);

        public IEnumerable<SaleOrderDTO> GetOrdersByUserId(int userId);
    }
}
