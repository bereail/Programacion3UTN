using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.ViewModel;
using Service.IServices;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _service;

        public EmpresaController(IEmpresaService service)
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

        [HttpPost("InsertEmpresa")]
        public ActionResult InsertEmpresa([FromBody] EmpresaViewModel empresa)
        {
            _service.InsertEmpresa(empresa);
            return Ok();
        }

        [HttpPut("UpdateEmpresa")]
        public ActionResult UpdateEmpresa([FromBody] EmpresaViewModel empresa)
        {
            _service.UpdateEmpresa(empresa);
            return Ok();
        }

        [HttpDelete("DeleteEmpresa")]
        public ActionResult DeleteEmpresa([FromQuery] int id)
        {
            _service.DeleteEmpresaById(id);
            return Ok();
        }
    }
}
