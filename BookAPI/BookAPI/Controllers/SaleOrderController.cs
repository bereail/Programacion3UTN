using Application.Data.Implementations;
using Application.Dtos.SaleOrderDTOs;
using Application.Interfaces.Services;
using Domain.Entities.Entities;
using Domain.Enums;
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
        private readonly IBookService _bookService;

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
                    return Unauthorized(new { message = "User is not authorized." });
                }             

                var saleOrders = _saleOrderService.GetOrdersByUserId(userId);

                if (saleOrders == null || !saleOrders.Any())
                {                   
                    return NotFound(new { message = "No sale orders found for this user." });
                }
                return Ok(saleOrders);
            }
            catch (Exception ex)
            {                
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }

        [HttpPost("Create")]
        public IActionResult AddSaleOrder(SaleOrderToCreateDTO saleOrderToCreateDTO)
        {
            try
            {
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(new { message = "User is not authorized." });
                }

                var newSaleOrder = _saleOrderService.CreateSaleOrder(saleOrderToCreateDTO, userId);

                if (newSaleOrder == null)
                {
                    return BadRequest(new { message = "Failed to create sale order." });
                }

                return Ok(newSaleOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An internal server error occurred.", error = ex.Message });
            }
        }




        [HttpDelete("{saleOrderId}")]
        public ActionResult DeleteSaleOrder(int saleOrderId)
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid();
            }
            var deletedSaleOrder = _saleOrderService.CancelSaleOrder(saleOrderId);
            if (deletedSaleOrder == null)
                return BadRequest();
            return Ok("Orden cancelada con Exito");
        }

    }
}
