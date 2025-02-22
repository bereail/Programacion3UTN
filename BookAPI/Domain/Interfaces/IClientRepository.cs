using Domain.Models.Entities;

namespace Domain.Interfaces
{
    public interface IClientRepository : IRepository
    {


        public ICollection<SaleOrder> GetClientSaleOrders(int clientId);
    }
}
