using Application.Dtos.SaleOrderDTOs;
using Domain.Entities.Entities;

namespace Application.Interfaces.Repository
{
    public interface ISaleOrderRepository : IRepository
    {
        public ICollection<SaleOrder> GetAllSaleOrders();
        public SaleOrder? GetSaleOrder(int SaleOrderId);
        public void AddSaleOrder(SaleOrder newSaleOrder);
       
        public void CancelSaleOrder(int saleOrderId);

        public IEnumerable<SaleOrder> GetOrdersByUserId(int userId);
    }
}
