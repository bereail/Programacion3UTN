using Application.Dtos.ClientDTOs;
using AutoMapper;
using Domain.Entities.Entities;

namespace Application.AutoMapperProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDTO>();
            CreateMap<ClientToCreateDTO, Client>();
            CreateMap<ClientToUpdateDTO, Client>();

            CreateMap<User, ClientDTO>();
            CreateMap<ClientToCreateDTO, User>();
            CreateMap<ClientToUpdateDTO, User>();
        }
    }
}
