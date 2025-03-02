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
        [HttpPost]
        public ActionResult<string> Login(LoginRequest loginRequest)
        {
            Console.WriteLine($"🔍 Intento de login para: {loginRequest.Email}");

            var user = _customAuthenticationService.ValidateUser(loginRequest); // Obtener usuario antes de generar token

            if (user == null)
            {
                Console.WriteLine("❌ Error: Usuario no encontrado o credenciales incorrectas.");
                return StatusCode(401);
            }

            Console.WriteLine($"✅ Usuario autenticado: {user.Email}, Rol: {user.Role}"); // Mostrar rol

            var token = _customAuthenticationService.Login(loginRequest);

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("❌ Error: No se generó el token.");
                return StatusCode(401);
            }

            Console.WriteLine("✅ Token generado exitosamente.");
            return Ok(token);
        }



    }
}
