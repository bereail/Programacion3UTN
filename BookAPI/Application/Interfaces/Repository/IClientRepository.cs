using Domain.Entities.Entities;

namespace Application.Interfaces.Repository
{
    public interface IClientRepository : IRepository
    {


        public ICollection<SaleOrder> GetClientSaleOrders(int clientId);
    }
}
