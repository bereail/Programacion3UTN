using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.ViewModel;
using Service.IServices;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly ICargoService _service;

        public CargoController(ICargoService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<EmpleadoDTO>> GetAll()
        {
            var response = _service.GetAll();
            if (response.Count == 0)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("InsertCargo")]
        public ActionResult InserCargo([FromBody] CargoViewModel cargo)
        {
            var response = _service.InsertCargo(cargo);
            if (response == null)
            {
                return BadRequest();
            }

            string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            string apiAndEndpointUrl = $"api/Cargo/GetCargo";
            string locationUrl = $"{baseUrl}/{apiAndEndpointUrl}/{response.Id}";
            return Created(locationUrl, response);
        }

        [HttpPut("UpdateCargo")]
        public ActionResult<string> UpdateCargo([FromBody] CargoViewModel cargo)
        {
            _service.UpdateCargo(cargo);
            return Ok();
        }

        [HttpDelete("DeleteCargo")]
        public ActionResult<string> DeleteCargo([FromQuery] int idEmpleado)
        {
            _service.DeleteCargoById(idEmpleado);
            return Ok();
        }
    }
}