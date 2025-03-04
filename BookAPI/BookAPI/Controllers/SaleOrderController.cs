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
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid();
            }
            var saleOrder = _saleOrderService.GetSaleOrder(saleOrderId);
            if (saleOrder == null)
                return NotFound();
            return Ok(saleOrder);
        }

        [HttpPost("Create")]

        public ActionResult AddSaleOrder(SaleOrderToCreateDTO saleOrderToCreateDTO)
        {

            // Verificar el contenido del JSON recibido
            Console.WriteLine("SaleOrderToCreateDTO received:");
            Console.WriteLine($"BookId: {saleOrderToCreateDTO.BookId}, BookQuantity: {saleOrderToCreateDTO.BookQuantity}");

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int clientId))
                return Unauthorized();

            var createdSaleOrder = _saleOrderService.AddSaleOrder(saleOrderToCreateDTO, clientId);

            if (createdSaleOrder == null)
                return BadRequest();

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
