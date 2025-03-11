using Application.Dtos.ClientDTOs;
using Application.Dtos.SaleOrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IClientService : IUserService
    {   

        public ICollection<SaleOrderDTO> GetClientSaleOrders(int clientId);  

    }
}
