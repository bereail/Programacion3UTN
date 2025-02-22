using AutoMapper;
using Domain.Interfaces;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Data.Implementations
{
    public class ClientServices
    {
        private readonly IClientRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
    public ClientServices(IClientRepository repository, IMapper mapper, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public ICollection<User> GetAllClients()
        {
            return _userRepository.GetAllUsers("Client");
        }

    }
}
