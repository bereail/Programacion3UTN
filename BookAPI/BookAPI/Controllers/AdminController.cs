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

        [HttpPut("{email}/disable")]
        public IActionResult DisableUser(string email)
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userRole != "Admin")
            {
                return Forbid();
            }

            var response = _userService.DisableAccount(email);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }



        [HttpPut("{email}/reactivate")]
        public IActionResult ReactivateUser(string email)
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid();
            }
            var response = _userService.ReactivateUser(email, User);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }
    }
}