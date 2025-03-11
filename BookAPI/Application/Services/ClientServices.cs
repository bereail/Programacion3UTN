using Application.Dtos.ClientDTOs;
using Application.Dtos.SaleOrderDTOs;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities.Entities;
using Domain.Enums;
using Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Application.Services
{
    public class ClientService : UserService, IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IUserRepository userRepository, IMapper mapper, IClientRepository clientRepository) : base(userRepository, mapper)
        {
            _clientRepository = clientRepository;
        }

        public ICollection<SaleOrderDTO> GetClientSaleOrders(int clientId)
        {
            var saleOrders = _clientRepository.GetClientSaleOrders(clientId);
            return _mapper.Map<ICollection<SaleOrderDTO>>(saleOrders);
        }

    }
}
