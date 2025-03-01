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
            _config = config; //Hacemos la inyección para poder usar el appsettings.json
            _customAuthenticationService = autenticacionService;
        }
        [HttpPost] //Vamos a usar un POST ya que debemos enviar los datos para hacer el login
        public ActionResult<string> Login(LoginRequest loginRequest) //Enviamos como parámetro la clase que creamos arriba
        {
            var token = _customAuthenticationService.Login(loginRequest); //Lo primero que hacemos es llamar a una función que valide los parámetros que enviamos.
            if (string.IsNullOrEmpty(token))
                return StatusCode(401);

            return Ok(token);
        }

    }
}
