using Application.Dtos.ClientDTOs;
using Application.Dtos.SaleOrderDTOs;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Enums;
using Domain.Models.Entities;
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


        public ClientDTO AddClient(ClientToCreateDTO clientToCreateDTO)
        {
            var newClient = _mapper.Map<Client>(clientToCreateDTO);
            _userRepository.AddUser(newClient);
            _userRepository.SaveChanges();
            return _mapper.Map<ClientDTO>(newClient);
        }
        public void UpdateClient(ClientToUpdateDTO clientToUpdateDTO, int clientId)
        {
            var clientToUpdate = _userRepository.GetUserById(clientId);
            _mapper.Map(clientToUpdateDTO, clientToUpdate);
            _userRepository.UpdateUser(clientToUpdate);
            _userRepository.SaveChanges();
        }

        public ICollection<SaleOrderDTO> GetClientSaleOrders(int clientId)
        {
            var saleOrders = _clientRepository.GetClientSaleOrders(clientId);
            return _mapper.Map<ICollection<SaleOrderDTO>>(saleOrders);
        }

    }
}
