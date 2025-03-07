using Application.Dtos.SaleOrderDTOs;
using Application.Interfaces.Repository;
using Domain.Entities.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infraestructure.Repositories
{
    public class SaleOrderRepository : Repository, ISaleOrderRepository
    {
        public SaleOrderRepository(ApplicationContext context, IBookRepository bookRepository) : base(context)
        {
        }

        public ICollection<SaleOrder> GetAllSaleOrders()
        {
            return _context.SaleOrders
                .OrderBy(s => s.Status)
                .Include(s => s.Book)
                .ToList();
        }

        public SaleOrder? GetSaleOrder(int SaleOrderId)
        {
            return _context.SaleOrders
                .Include(s => s.Book)
                .FirstOrDefault(s => s.SaleOrderId == SaleOrderId);
        }
        public void AddSaleOrder(SaleOrder newSaleOrder)
        {
            _context.SaleOrders.Add(newSaleOrder);
            _context.SaveChanges();
        }

        public void DeleteSaleOrder(int saleOrderId)
        {
            var saleOrder = _context.SaleOrders.Find(saleOrderId);
            if (saleOrder != null)
                _context.SaleOrders.Remove(saleOrder);
        }

        public IEnumerable<SaleOrder> GetOrdersByUserId(int userId)
        {
            return _context.SaleOrders.Where(o => o.ClientId == userId).ToList();
        }

      
    }
}
