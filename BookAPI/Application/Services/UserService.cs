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



        //OK
        public User? GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
                throw new Exception("Usuario no encontrado.");
            return user;
        }

        //OK
        public User? GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email no puede estar vacío.");

            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new KeyNotFoundException("Usuario no encontrado.");

            return user;
        }

        //OK
        public List<UserDto> GetUsers()
        {
            var users = _userRepository.GetAllUsers();

            if (users == null || users.Count == 0)
                throw new Exception("No se encontraron usuarios.");

            // Usa AutoMapper para mapear los datos a UserDto
            return _mapper.Map<List<UserDto>>(users);
        }


        //OK
        public IEnumerable<AdminDTO> GetAdmins()
        {
            var admins = _userRepository.GetUsersByRole(UserRole.Admin);
            return _mapper.Map<List<AdminDTO>>(admins) ;
        }

        //OK
        public IEnumerable<ClientDTO> GetClients()
        {
            var clients = _userRepository.GetUsersByRole(UserRole.Client);
            return _mapper.Map<List<ClientDTO>>(clients);
        }


        //OK
        public void DisableAccount(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                throw new Exception("Usuario no encontrado.");

            user.IsActive = false;
            _userRepository.UpdateUser(user);
        }

        //OK
        public BaseResponse ReactivateUser(int idUser, ClaimsPrincipal user)
        {
            var existingUser = _userRepository.GetUserById(idUser);
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




        public User? ValidateUser(AuthenticationRequestBody authenticationRequestBody)
        {
            return _userRepository.ValidateUser(authenticationRequestBody);
        }


        public BaseResponse Login(string mail, string password)
        {
            throw new NotImplementedException();
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