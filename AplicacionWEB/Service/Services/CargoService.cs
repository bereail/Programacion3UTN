using Microsoft.EntityFrameworkCore;
using Model.DTO;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CargoService : ICargoService
    {
        private readonly OrganizacionContext _context;

        public CargoService(OrganizacionContext _context)
        {
            this._context = _context;
        }

        public List<CargoDTO> GetAll()
        {
            return _context.Cargo.ToList().Select(s => new CargoDTO() { Id = s.Id, Descripcion = s.Descripcion, Salario = s.Salario }).ToList();
        }

        public CargoDTO InsertCargo(CargoViewModel cargo)
        {
            _context.Cargo.Add(new Cargo()
            {
                Descripcion = cargo.Descripcion,
                Salario = cargo.Salario
            });
            _context.SaveChanges();

            var lastCargo = _context.Cargo.OrderBy(x => x.Id).Last();
            CargoDTO response = new CargoDTO()
            {
                Id = lastCargo.Id,
                Descripcion = lastCargo.Descripcion,
                Salario = lastCargo.Salario
            };

            return response;
        }

        public void UpdateCargo(CargoViewModel cargo)
        {
            Cargo cargoDatabase = _context.Cargo.First(f => f.Id == cargo.Id);

            cargoDatabase.Descripcion = cargo.Descripcion;
            cargoDatabase.Salario = cargo.Salario;
            _context.SaveChanges();
        }

        public void DeleteCargoById(int id)
        {
            _context.Cargo.Remove(_context.Cargo.First(f => f.Id == id));
            _context.SaveChanges();
        }
    }
}
