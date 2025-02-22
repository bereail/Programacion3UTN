using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userService;

        public AuthenticationService(IUserRepository userService)
        {
            _userService = userService;
        }


        //---------------------------------------------------------------------------------------------------------------------------------------
        public User? ValidateUser(AuthenticationRequestBody authenticationRequestBody)
        {
            if (string.IsNullOrEmpty(authenticationRequestBody.Email) || string.IsNullOrEmpty(authenticationRequestBody.Password))
                return null;

            var user = _userService.ValidateUser(authenticationRequestBody);

            if (user is null || !user.IsActive)
            {
                return null;
            }

            return user;
        }



    }
}