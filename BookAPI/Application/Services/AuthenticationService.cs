using Application.Models.Requests;
using Domain.Entities;

using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;

using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Entities.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Services
{
    public class AuthenticationService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticacionServiceOptions _options;

        public AuthenticationService(IUserRepository userRepository, IOptions<AuthenticacionServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

       

        public string? Login(LoginRequest rq)
        {
            
            var user = ValidateUser(rq);

            if (user == null)
            {
              
                throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");
            }

            if (!user.IsActive) 
            {
             
                throw new UnauthorizedAccessException("El usuario no está activo.");
            }
            //Paso 2: Crear el token
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey)); //Traemos la SecretKey del Json. agregar antes: using Microsoft.IdentityModel.Tokens;

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);
          

            //Los claims son datos en clave->valor que nos permite guardar data del usuario.
            var claimsForToken = new List<Claim>
{
             new(ClaimTypes.NameIdentifier, user.Id.ToString()),
             new(ClaimTypes.Role, user.Role.ToString()) // ✅ Convertir a string
};

            var jwtSecurityToken = new JwtSecurityToken( //agregar using System.IdentityModel.Tokens.Jwt; Acá es donde se crea el token con toda la data que le pasamos antes.
              _options.Issuer,
              _options.Audience,
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              credentials);

            var tokenToReturn = new JwtSecurityTokenHandler() //Pasamos el token a string
                .WriteToken(jwtSecurityToken);


            return tokenToReturn.ToString();
        }

        public User? ValidateUser(LoginRequest rq)
        {
            if (string.IsNullOrEmpty(rq.Email) || string.IsNullOrEmpty(rq.Password))
                return null;

            var user = _userRepository.GetUser(rq.Email, rq.Password);

            if (user != null)
            {
                return user;
            }
          
            return null;
        }




        public class AuthenticacionServiceOptions
        {
            public const string AuthenticacionService = "AuthenticacionService";

            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }
    }
}
