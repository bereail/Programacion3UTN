using Application.Dtos.ClientDTOs;
using Application.Dtos.SaleOrderDTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookAPI.Controllers
{
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IUserService _userService;

        public ClientController(IClientService clientService, IUserService userService)
        {
            _clientService = clientService;
            _userService = userService;
        }



        [HttpGet("{id}/GetSaleOrders")]
        public ActionResult<ICollection<SaleOrderDTO>> GetClientSaleOrders(int id)
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid(); 
            }

            var saleOrders = _clientService.GetClientSaleOrders(id);
            return Ok(saleOrders);
        }


        [HttpPut("Update/{id}")]

        public ActionResult UpdateClient([FromBody] ClientToUpdateDTO clientToUpdate)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            _clientService.UpdateClient(clientToUpdate, userId);
            return NoContent();
        }

    }
}