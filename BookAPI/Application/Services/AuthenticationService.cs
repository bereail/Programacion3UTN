using Application.Interfaces.Services;
using Domain.Models.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repository;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------
        public User? ValidateUser(AuthenticationRequestBody authenticationRequestBody)
        {
            if (string.IsNullOrEmpty(authenticationRequestBody.Email) || string.IsNullOrEmpty(authenticationRequestBody.Password))
                return null;

            var user = _userRepository.GetUserByEmail(authenticationRequestBody.Email);

            if (user is null || !user.IsActive || user.Password != authenticationRequestBody.Password)
            {
                return null;
            }

            return user;
        }

    }
}