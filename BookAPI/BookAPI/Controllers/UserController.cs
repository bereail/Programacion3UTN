using Application.Data.Services;
using Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        public UserController(UserService service)
        {
            _service = service;
        }


        //Ingresando un rol, pueden verde los users correspondientes
        [HttpGet("role/{role}")]
        public IActionResult Get([FromRoute] string role)
        {
            return Ok(_service.GetAllUsers(role));
        }


        [HttpGet("userid/{userId}")]
        public IActionResult GetUserById([FromRoute] int userId)
        {
            try
            {
                var userDto = _service.GetUserById(userId);
                if (userDto == null)
                {
                    return NotFound();  
                }
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{username}")]
        public IActionResult GetUserByUsername([FromRoute] string username)
        {
            try
            {
                var userDto = _service.GetUserByUsername(username);
                if (userDto == null)
                {
                    return NotFound();  
                }
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}