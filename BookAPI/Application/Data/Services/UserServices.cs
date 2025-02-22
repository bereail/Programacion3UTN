using Application.Dtos.UserDto;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public IEnumerable<UserDto> GetAllUsers(string role)
        {
            var users = _repository.GetAllUsers(role);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public UserDto GetUserById(int userId)
        {
            var user = _repository.GetUserById(userId);
            return _mapper.Map<UserDto>(user);
        }

        public UserDto GetUserByUsername(string username)
        {
            var user = _repository.GetUserByUsername(username);
            return _mapper.Map<UserDto>(user);
        }

        public int AddUser(UserForAddRequest request)
        {
            var user = _mapper.Map<User>(request);  
            return _repository.AddUser(user);
        }

        public void UpdateUser(int userId, UserForUpdateRequest request)
        {
            var user = _repository.GetUserById(userId);
            if (user == null) throw new Exception("User not found.");

            _mapper.Map(request, user);  
            _repository.UpdateUser(user);
        }

        public void DeleteUser(int userId)
        {
            _repository.DeleteUser(userId);
        }
    }
}