using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data.Repositories
{
    public class UserServices : IUserService
    {
        internal readonly IUserRepository _userRepository;
        internal readonly IMapper _mapper;

        public UserServices(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
            _userRepository.SaveChanges();
        }

        public User? ValidateUser(AuthenticationRequestBody authenticationRequestBody)
        {
            throw new NotImplementedException();
        }
    }
}