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
        SaleOrderDTO? GetSaleOrder(int SaleOrderId);
        ICollection<SaleOrderDTO> GetAllSaleOrders();
       SaleOrderDTO CreateSaleOrder(SaleOrderToCreateDTO saleOrderToCreateDTO, int clientId);

        SaleOrderStatusDTO? UpdateSaleOrderStatus(int saleOrderId);
        SaleOrderDTO? CancelSaleOrder(int saleOrderId);

        IEnumerable<SaleOrderDTO> GetOrdersByUserId(int userId);
    }
}
