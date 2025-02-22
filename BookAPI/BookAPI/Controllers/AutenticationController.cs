using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System;
using Domain.Models.Entities;
using Domain.Models;
using Domain.Interfaces;
using Application.Data.Services;
using AutoMapper;

namespace BookAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AutenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserComparisonRepository _userComparisonService;

        public AutenticationController(IConfiguration config, IAuthenticationService authenticationService, IUserComparisonRepository userComparisonService)
        {
            _config = config;
            _authenticationService = authenticationService;
            _userComparisonService = userComparisonService;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {

            var user = ValidateCredentials(authenticationRequestBody);


            if (user is null)
            {
                return Unauthorized("Credenciales no válidas");
            }

            if (!user.IsActive)
            {
                return Unauthorized("La cuenta está deshabilitada");
            }


            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.Email));
            claimsForToken.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
              _config["Authentication:Issuer"],
              _config["Authentication:Audience"],
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              credentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private User? ValidateCredentials(AuthenticationRequestBody authenticationRequestBody)
        {
            return _authenticationService.ValidateUser(authenticationRequestBody);
        }



    }
}