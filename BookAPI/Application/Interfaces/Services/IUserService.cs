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
        // LOGIN
    
        BaseResponse Login(string mail, string password);
        User? ValidateUser(AuthenticationRequestBody authenticationRequestBody);

        // GESTIÓN DE USUARIOS
        //OK
        User? GetUserById(int id);

        //OK
        User? GetUserByEmail(string email);

        //OK
        List<UserDto> GetUsers();

        //OK
        IEnumerable<AdminDTO> GetAdmins();

        //OK
        IEnumerable<ClientDTO> GetClients();

        // OK
        void DisableAccount(int userId);

        //OK
        BaseResponse ReactivateUser(int idUser, ClaimsPrincipal user);

        // BOOKING ASOCIADOS AL USUARIO
        List<int> GetBookingIdsByUserId(int userId);
    }
}
