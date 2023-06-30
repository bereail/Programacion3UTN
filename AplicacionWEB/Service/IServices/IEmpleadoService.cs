using Model.DTO;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IEmpleadoService
    {
        List<EmpleadoDTO> GetListadoEmpleados();
        EmpleadoDTO GetEmpleadoById(int id);
        List<EmpleadoOrganizacionDTO> GetListadoEmpleadoOrganizacion();
        EmpleadoDTO InsertEmpleado(EmpleadoViewModel empleado);
        EmpleadoDTO UpdateEmpleado(EmpleadoViewModel empleado);
        void DeleteEmpleado(int id);
    }
}