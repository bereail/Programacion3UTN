using AutoMapper;
using Model.DTO;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using Service.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly OrganizacionContext _context;
        private readonly IMapper _mapper;

        public EmpleadoService(OrganizacionContext _context)
        {
            this._context = _context;
            _mapper = AutoMapperConfig.Configure();
        }

        public List<EmpleadoDTO> GetListadoEmpleados()
        {
            return _mapper.Map<List<EmpleadoDTO>>(_context.Empleado.ToList());
        }

        public EmpleadoDTO GetEmpleadoById(int id)
        {
            return _mapper.Map<EmpleadoDTO>(_context.Empleado.Where(x => x.Id == id).First());
        }

        public List<EmpleadoOrganizacionDTO> GetListadoEmpleadoOrganizacion()
        {
            List<EmpleadoOrganizacionDTO> empleados = (from empleado in _context.Empleado
                                                       join empresa in _context.Empresa
                                                       on empleado.IdEmpresa equals empresa.Id
                                                       join cargo in _context.Cargo
                                                       on empleado.IdCargo equals cargo.Id
                                                       select new EmpleadoOrganizacionDTO()
                                                       {
                                                           NombreEmpleado = empleado.Nombre,
                                                           CargoEmpleado = cargo.Descripcion,
                                                           SalarioEmpleado = cargo.Salario,
                                                           RazonSocialEmpresa = empresa.RazonSocial
                                                       }).ToList();

            return empleados;
        }

        public EmpleadoDTO InsertEmpleado(EmpleadoViewModel empleado)
        {
            _context.Empleado.Add(new Empleado()
            {
                Nombre = empleado.Nombre,
                IdCargo = _context.Cargo.First(f => f.Id == empleado.IdCargo).Id,
                IdEmpresa = _context.Empresa.First(f => f.Id == empleado.IdEmpresa).Id
            });
            _context.SaveChanges();

            var lastEmpleado = _context.Empleado.OrderBy(x => x.Id).Last();
            
            return _mapper.Map<EmpleadoDTO>(lastEmpleado);
        }

        public EmpleadoDTO UpdateEmpleado(EmpleadoViewModel empleado)
        {
            Empleado empleadoDataBase = _context.Empleado.Single(f => f.Id == empleado.Id);
            empleadoDataBase.Nombre = empleado.Nombre;
            empleadoDataBase.IdCargo = _context.Cargo.First(f => f.Id == empleado.IdCargo).Id;
            empleadoDataBase.IdEmpresa = _context.Empresa.First(f => f.Id == empleado.IdEmpresa).Id;
            _context.SaveChanges();

            var lastEmpleado = _context.Empleado.OrderBy(x => x.Id).Last();

            return _mapper.Map<EmpleadoDTO>(lastEmpleado);
        }

        public void DeleteEmpleado(int id)
        {
            _context.Empleado.Remove(_context.Empleado.Single(f => f.Id == id));
            _context.SaveChanges();
        }
    }
}