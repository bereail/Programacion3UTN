/*using Application.Dtos.AdminDTOs;
using Domain.Entities.Dtos.AdminDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;

        public AdminController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        [HttpGet("GetAllAdmins")]
        public ActionResult<ICollection<AdminDTO>> GetAllAdmins()
        {
            var admins = _adminService.GetAllAdmins();
            return Ok(admins);
        }

        [HttpGet("GetAdminById")]
        public ActionResult<AdminDTO> GetAdminById()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var admin = _adminService.GetAdminById(userId);
            if (admin == null)
                return NotFound();

            return Ok(admin);
        }

        [HttpPost("SingIn")]
        [AllowAnonymous]
        public ActionResult AddAdmin(AdminToCreateDTO admin)
        {
            var createdAdmin = _adminService.AddAdmin(admin);
            return NoContent();
        }

        [HttpPut("Update")]
        public ActionResult UpdateAdmin(AdminToUpdateDTO admin)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            _adminService.UpdateAdmin(admin, userId);
            return Ok("Usuario actualizado con exito");
        }

        [HttpDelete("Delete")]
        public ActionResult DeleteAdmin()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userid))
                return Unauthorized();

            _userService.DeleteUser(userid);
            return Ok("Usuario eliminado con exito");
        }
    }
}
*/