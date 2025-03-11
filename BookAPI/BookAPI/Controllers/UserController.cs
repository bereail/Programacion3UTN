using Application.Dtos.AdminDTOs;
using Application.Dtos.ClientDTOs;
using Application.Dtos.SaleOrderDTOs;
using Application.Dtos.UserDto;
using Application.Interfaces.Services;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid(); 
            }

            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound(new { message = "Usuario no encontrado." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor.", error = ex.Message });
            }
        }



        [HttpPost("SignIn")]
        [AllowAnonymous]
        public IActionResult AddClient(ClientToCreateDTO clientToCreate)
        {
            try
            {
                _userService.AddClient(clientToCreate);
                return Created("", new { message = "Usuario creado exitosamente" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }
       
    }
}