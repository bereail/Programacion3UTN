using Api.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using Service.Services;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _service;
        private readonly ILogger<EmpleadoController> _logger;

        public EmpleadoController(IEmpleadoService service, ILogger<EmpleadoController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<EmpleadoDTO>> GetListado()
        {
            try
            {
                var response = _service.GetListadoEmpleados();
                if (response.Count == 0)
                {
                    NotFound("No existe ningun empleado");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogCustomError("GetAll", ex);
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetEmpleadoById/{id}")]
        public ActionResult<EmpleadoDTO> GetEmpleadoById([FromRoute] int id)
        {
            try
            {
                var response = _service.GetEmpleadoById(id);
                if (response == null)
                {
                    return NotFound($"No se encontro el empleado con id {id}");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogCustomError("GetEmpleadoById", ex);
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetListadoEmpleadoOrganizacion")]
        public ActionResult<List<EmpleadoOrganizacionDTO>> GetListadoEmpleadoOrganizacion()
        {
            try
            {
                var response = _service.GetListadoEmpleadoOrganizacion();
                if (response.Count == 0)
                {
                    return NotFound("No se pudo obtener el listado");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogCustomError("GetListadoEmpleadoOrganizacion", ex);
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("InsertEmpleado")]
        public ActionResult<EmpleadoDTO> InsertEmpleado([FromBody] EmpleadoViewModel empleado)
        {
            try
            {
                var response = _service.InsertEmpleado(empleado);

                string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                string apiAndEndpointUrl = $"api/Empleado/GetEmpleadoById";
                string locationUrl = $"{baseUrl}/{apiAndEndpointUrl}/{response.Id}";
                return Created(locationUrl, response);
            }
            catch (Exception ex)
            {
                _logger.LogCustomError("InsertEmpleado", ex);
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("UpdateEmpleado")]
        public ActionResult<EmpleadoDTO> UpdateEmpleado([FromBody] EmpleadoViewModel empleado)
        {
            try
            {
                var response = _service.UpdateEmpleado(empleado);

                string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                string apiAndEndpointUrl = $"api/Empleado/GetEmpleadoById";
                string locationUrl = $"{baseUrl}/{apiAndEndpointUrl}/{response.Id}";
                return Created(locationUrl, response);
            }
            catch (Exception ex)
            {
                _logger.LogCustomError("UpdateEmpleado", ex);
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteEmpleado/{id}")]
        public ActionResult DeleteEmpleado([FromRoute] int id)
        {
            try
            {
                _service.DeleteEmpleado(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCustomError("DeleteEmpleado", ex);
                return BadRequest($"{ex.Message}");
            }
        }
    }
}