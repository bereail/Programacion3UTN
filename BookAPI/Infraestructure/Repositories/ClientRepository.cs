using Application.Dtos.SaleOrderDTOs;
using Application.Interfaces.Repository;
using Domain.Entities.Entities;
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
            var client = _context.Clients
                .Include(c => c.SaleOrders) 
                .ThenInclude(s => s.Book)    
                .FirstOrDefault(c => c.Id == clientId);

            if (client == null)
            {
                throw new KeyNotFoundException($"No se encontró un cliente con el ID {clientId}.");
            }

            if (client.SaleOrders == null || !client.SaleOrders.Any())
            {
                throw new InvalidOperationException($"El cliente con ID {clientId} no tiene órdenes de venta.");
            }

            return client.SaleOrders;
        }
    }
}
