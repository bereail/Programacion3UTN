using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Models;
using Application.Models.Requests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(IConfiguration config, ICustomAuthenticationService autenticacionService)
        {
            _config = config;
            _customAuthenticationService = autenticacionService;
        }
        [HttpPost]
        public ActionResult<string> Login(LoginRequest loginRequest)
        {
            var user = _customAuthenticationService.ValidateUser(loginRequest); 

            if (user is null)
            {
              
                return Unauthorized("Credenciales no válidas"); 
            }                    

            if (!user.IsActive)
            {
           
                return Unauthorized("La cuenta está deshabilitada");
            }

            var token = _customAuthenticationService.Login(loginRequest);

            if (string.IsNullOrEmpty(token))
            {
                return StatusCode(401, ("❌ Error: No se generó el token."));
            }

            return Ok(token);
        }


    }
}
