using Application.Dtos.AdminDTOs;
using Application.Dtos.ClientDTOs;
using Application.Dtos.UserDto;
using Application.Dtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Enums;
using System;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Domain.Entities;
using Domain.Entities.Entities;

namespace Infraestructure.Data.Repositories
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IMapper _mapper;

        public UserDto? GetUser(string email, string password)
        {
            var user = _userRepository.GetUser(email, password);

            return user == null ? null : new UserDto { Email = user.Email };
        }
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public User? GetUserById(int id)
        {
            return _userRepository.GetUserById(id); 
        }


        public User? GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email no puede estar vacío.");

            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new KeyNotFoundException("Usuario no encontrado.");

            return user;
        }


        public List<UserDto> GetUsers()
        {
            var users = _userRepository.GetAllUsers();

            if (users == null || users.Count == 0)
                throw new Exception("No se encontraron usuarios.");

            
            return _mapper.Map<List<UserDto>>(users);
        }



        public IEnumerable<AdminDTO> GetAdmins()
        {
            var admins = _userRepository.GetUsersByRole(UserRole.Admin);
            return _mapper.Map<List<AdminDTO>>(admins) ;
        }


        public IEnumerable<ClientDTO> GetClients()
        {
            var clients = _userRepository.GetUsersByRole(UserRole.Client);
            return _mapper.Map<List<ClientDTO>>(clients);
        }


        public BaseResponse DisableAccount(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new Exception("Usuario no encontrado.");

            if (user.Role.ToString() == "Admin")
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Por jerarquía, no se puede desactivar a otro admin."
                };
            }


            user.IsActive = false;
            _userRepository.UpdateUser(user);

            return new BaseResponse
            {
                Success = true,
                Message = "Cuenta desactivada exitosamente."
            };
        }

        public BaseResponse ReactivateUser(string email, ClaimsPrincipal user)
        {
            var existingUser = _userRepository.GetUserByEmail(email);
            if (existingUser == null)
                return new BaseResponse { Success = false, Message = "Usuario no encontrado." };


            existingUser.IsActive = true;
            _userRepository.UpdateUser(existingUser);

            return new BaseResponse { Success = true, Message = "Cuenta reactivada exitosamente." };
        }


        public List<int> GetBookingIdsByUserId(int userId)
        {
            return _userRepository.GetBookingIdsByUserId(userId);
        }

        public ClientDTO AddClient(ClientToCreateDTO clientToCreateDTO)
        {
            if (string.IsNullOrWhiteSpace(clientToCreateDTO.Email))
            {
                throw new ArgumentException("El email no puede estar vacío.");
            }

            var existingUser = _userRepository.GetUserByEmail(clientToCreateDTO.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("El correo electrónico ya está registrado.");
            }

            var newClient = _mapper.Map<Client>(clientToCreateDTO);
            _userRepository.AddUser(newClient);
            _userRepository.SaveChanges();
            return _mapper.Map<ClientDTO>(newClient);
        }


    }
}