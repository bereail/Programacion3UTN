using Application.Data.Implementations;
using Application.Data.Services;
using Application.Dtos.ClientDTOs;
using Application.Dtos.SaleOrderDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace BookAPI.Controllers
{
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        private readonly ClientServices _clientService;
        private readonly UserService _userService;

        public ClientController(ClientServices clientService, UserService userService)
        {
            _clientService = clientService;
            _userService = userService;
        }
        [HttpGet("GetAllClients")]
        [Authorize(Roles = "Admin")]  // Solo Admin puede ver todos los clientes
        public ActionResult<ICollection<ClientDTO>> GetAllClients()
        {
            try
            {
                var clients = _userService.GetAllUsers("Client");
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /* [HttpGet("{id}/GetSaleOrders")]
         [Authorize(Roles = "Admin, Client")]
         public ActionResult<ICollection<SaleOrderDTO>> GetClientSaleOrders(int id)
         {
             var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

             var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
             if (!int.TryParse(userIdClaim, out int userId))
                 return Unauthorized();

             if (userId != id && userRole != "Admin")
                 return Forbid();

             var saleOrders = _clientService.GetClientSaleOrders(id);
             return Ok(saleOrders);

         }

         [HttpPost("SingIn")]
         [AllowAnonymous]
         public ActionResult<ClientDTO> AddClient(ClientToCreateDTO clientToCreate)
         {
             var createdClient = _clientService.AddClient(clientToCreate);
             return CreatedAtRoute("GetClient", new { id = createdClient.Id }, createdClient);
         }

         [HttpPut("Update")]
         [Authorize(Roles = "Client")]
         public ActionResult UpdateClient([FromBody] ClientToUpdateDTO clientToUpdate)
         {
             var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

             if (!int.TryParse(userIdClaim, out int userId))
                 return Unauthorized();

             _clientService.UpdateClient(clientToUpdate, userId);
             return NoContent();
         }

         [HttpDelete("Delete")]
         [Authorize(Roles = "Client")]
         public ActionResult DeleteClient()
         {
             var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

             if (!int.TryParse(userIdClaim, out int userId))
                 return Unauthorized();

             _userService.DeleteUser(userId);
             return NoContent();
         }*/

    }
    }
