using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.AdminDTOs;
using Application.Dtos.ClientDTOs;
using Application.Dtos.UserDto;
using System.Security.Claims;
using Application.Dtos;
using Domain.Entities;
using Domain.Entities.Entities;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {            
        BaseResponse Login(string mail, string password);     
        User? GetUserById(int id);
        User? GetUserByEmail(string email);
        List<UserDto> GetUsers();
        IEnumerable<AdminDTO> GetAdmins();
        IEnumerable<ClientDTO> GetClients();
        BaseResponse DisableAccount(string email);
        BaseResponse ReactivateUser(string email, ClaimsPrincipal user);
        List<int> GetBookingIdsByUserId(int userId);
        public ClientDTO AddClient(ClientToCreateDTO clientToCreateDTO);

      
    }
}


/* User? ValidateUser(AuthenticationRequestBody authenticationRequestBody);*/