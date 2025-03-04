using Application.Dtos.AdminDTOs;
using Application.Dtos.ClientDTOs;
using Application.Dtos.UserDto;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IClientService _clientService;

        public AdminController(IUserService userService, IClientService clientService)
        {
            _userService = userService;
            _clientService = clientService; 
        }

        [HttpGet("{id}/GetUserById")]
        public IActionResult GetUserById(int id)
        {

            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid();
            }
            try
            {
                var user = _userService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{email}/GetUserByEmail")]
        public IActionResult GetUserByEmail(string email)
        {

            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid();
            }
            try
            {
                var user = _userService.GetUserByEmail(email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }




        [HttpGet("GetAllClients")]
        public ActionResult<ICollection<ClientDTO>> GetAllClients()
        {

            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid();
            }
            var clients = _userService.GetClients();
            return Ok(clients);
        }


        [HttpGet("GetAllAdmins")]
        public ActionResult<ICollection<AdminDTO>> GetAllAdmins()
        {

            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid();
            }
            var admins = _userService.GetAdmins();
            return Ok(admins);
        }


        [HttpPut("{id}/disable")]
        public IActionResult DisableUser(int id)
        {

            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid();
            }
            _userService.DisableAccount(id);
            return NoContent();
        }


        [HttpPut("{id}/reactivate")]
        public IActionResult ReactivateUser(int id)
        {

            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid();
            }
            var response = _userService.ReactivateUser(id, User);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }

    }
}