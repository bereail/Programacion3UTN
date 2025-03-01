using Application.Dtos.AdminDTOs;
using Application.Dtos.ClientDTOs;
using Application.Dtos.SaleOrderDTOs;
using Application.Dtos.UserDto;
using Application.Interfaces.Services;
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

        //OK
        [HttpGet("{id}/GetUserById")]
        public IActionResult GetUserById(int id)
        {
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

        //OK
        [HttpGet("{email}/GetUserByEmail")]
        public IActionResult GetUserByEmail(string email)
        {
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

        //OK
        [HttpGet("all")]
        public ActionResult<List<UserDto>> GetUsers()
        {
            try
            {
                var users = _userService.GetUsers();
                return Ok(users); // 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los usuarios.", error = ex.Message });
            }
        }


        //OK
        [HttpPut("{id}/reactivate")]
        public IActionResult ReactivateUser(int id)
        {
            var response = _userService.ReactivateUser(id, User);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }


        //OK
        [HttpPut("{id}/disable")]
        public IActionResult DisableUser(int id)
        {
            _userService.DisableAccount(id);
            return NoContent();
        }

        //OK

        [HttpGet("GetAllClients")]
        /*[Authorize(Roles = "Admin, Client")]*/
        public ActionResult<ICollection<ClientDTO>> GetAllClients()
        {
            var clients = _userService.GetClients();
            return Ok(clients);
        }

        //OK
        [HttpGet("GetAllAdmins")]
        /*[Authorize(Roles = "Admin, Client")]*/
        public ActionResult<ICollection<AdminDTO>> GetAllAdmins()
        {
            var admins = _userService.GetAdmins();
            return Ok(admins);
        }


        [HttpGet("{id}/GetSaleOrders")]
        [Authorize(Roles = "Admin, Client")]
        public ActionResult<ICollection<SaleOrderDTO>> GetClientSaleOrders(int id)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            if (userId != id && userRole != "Admin")
                return Forbid();

            var saleOrders = _userService.GetBookingIdsByUserId(id);
            return Ok(saleOrders);

        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticationRequestBody request)
        {
            var response = _userService.Login(request.Email, request.Password);
            if (!response.Success)
                return Unauthorized(response.Message);

            return Ok(response);
        }

    }
}