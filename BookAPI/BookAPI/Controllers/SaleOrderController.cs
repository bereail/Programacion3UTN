using Application.Dtos.SaleOrderDTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookAPI.Controllers
{
    [ApiController]
    [Route("api/saleOrder")]
    public class SaleOrderController : ControllerBase
    {
        private readonly ISaleOrderService _saleOrderService;

        public SaleOrderController(ISaleOrderService saleOrderService)
        {
            _saleOrderService = saleOrderService;
        }

        [HttpGet("GetAll")]
        public ActionResult<SaleOrderDTO> GetAllSaleOrders()
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid();
            }
            var saleOrders = _saleOrderService.GetAllSaleOrders();
            return Ok(saleOrders);
        }


        [HttpGet("{saleOrderId}", Name = "GetSaleOrder")]
        public ActionResult<SaleOrderDTO> GetSaleOrder(int saleOrderId)
        {
            try
            {
                var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                if (userRole != "Admin")
                {
                    Console.WriteLine($"Access denied for user. Role: {userRole}");
                    return Forbid();
                }

                if (saleOrderId <= 0)
                {
                    Console.WriteLine($"Error: Invalid saleOrderId {saleOrderId}");
                    return BadRequest(new { message = "Invalid sale order ID." });
                }

                var saleOrder = _saleOrderService.GetSaleOrder(saleOrderId);

                if (saleOrder == null)
                {
                    Console.WriteLine($"Error: Sale order with ID {saleOrderId} not found.");
                    return NotFound(new { message = $"Sale order with ID {saleOrderId} not found." });
                }

                return Ok(saleOrder);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }


        [HttpGet("MyOrders")]
        public ActionResult<IEnumerable<SaleOrderDTO>> GetMyOrders()
        {
            try
            {
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    Console.WriteLine("Unauthorized access: User ID is missing or invalid.");
                    return Unauthorized(new { message = "User is not authorized." });
                }

                Console.WriteLine($"Fetching sale orders for user ID: {userId}");

                var saleOrders = _saleOrderService.GetOrdersByUserId(userId);

                if (saleOrders == null || !saleOrders.Any())
                {
                    Console.WriteLine($"No sale orders found for user ID {userId}.");
                    return NotFound(new { message = "No sale orders found for this user." });
                }

                return Ok(saleOrders);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }



        [HttpPost("Create")]
        public ActionResult AddSaleOrder(SaleOrderToCreateDTO saleOrderToCreateDTO)
        {
            Console.WriteLine("SaleOrderToCreateDTO received:");
            Console.WriteLine($"BookId: {saleOrderToCreateDTO.BookId}, BookQuantity: {saleOrderToCreateDTO.BookQuantity}");

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int clientId))
            {
                Console.WriteLine("Error: Unauthorized - Invalid user ID");
                return Unauthorized(new { message = "Unauthorized: Invalid user ID" });
            }

            var createdSaleOrder = _saleOrderService.AddSaleOrder(saleOrderToCreateDTO, clientId);

            if (createdSaleOrder == null)
            {
                Console.WriteLine("Error: Sale order could not be created (possible stock issue)");
                return BadRequest(new { message = "Error: Insufficient stock or invalid book ID." });
            }

            return CreatedAtRoute("GetSaleOrder", new { saleOrderId = createdSaleOrder.SaleOrderId }, createdSaleOrder);
        }




        [HttpPut("{saleOrderId}")]
        public ActionResult<SaleOrderStatusDTO> ChangeSaleOrderStatus(int saleOrderId)
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid();
            }
            var newStatus = _saleOrderService.UpdateSaleOrderStatus(saleOrderId);
            if (newStatus == null)
                return NotFound();
            return Ok("Estado de orden: " + newStatus);
        }


        [HttpDelete("{saleOrderId}")]
        public ActionResult DeleteSaleOrder(int saleOrderId)
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid();
            }
            var deletedSaleOrder = _saleOrderService.DeleteSaleOrder(saleOrderId);
            if (deletedSaleOrder == null)
                return BadRequest();
            return Ok("Orden eliminada con Exito");
        }

    }
}
