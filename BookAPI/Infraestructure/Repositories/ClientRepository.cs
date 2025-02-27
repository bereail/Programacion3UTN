using Application.Dtos.SaleOrderDTOs;
using Application.Interfaces.Repository;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infraestructure.Repositories
{
    public class ClientRepository : Repository, IClientRepository
    {
        public ClientRepository(ApplicationContext context) : base(context)
        {
        }

        public ICollection<SaleOrder> GetClientSaleOrders(int clientId)
        {
            return _context.Clients
                 .Where(c => c.Id == clientId)
                 .SelectMany(c => c.SaleOrders)
                 .Include(s => s.Book)
                 .ToList();
        }
    }
}
