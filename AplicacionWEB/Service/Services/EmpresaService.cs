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
    public class EmpresaService : IEmpresaService
    {
        private readonly OrganizacionContext _context;

        public EmpresaService(OrganizacionContext _context)
        {
            this._context = _context;
        }

        public List<EmpresaDTO> GetAll()
        {
            return _context.Empresa.ToList().Select(s => new EmpresaDTO() { Id = s.Id, RazonSocial = s.RazonSocial }).ToList();
        }

        public EmpresaDTO InsertEmpresa(EmpresaViewModel empresa)
        {
            _context.Empresa.Add(new Empresa() { RazonSocial = empresa.RazonSocial });
            _context.SaveChanges();

            var lastEmpresa = _context.Empresa.OrderBy(x => x.Id).Last();
            EmpresaDTO response = new EmpresaDTO()
            {
                Id = lastEmpresa.Id,
                RazonSocial = lastEmpresa.RazonSocial
            };

            return response;
        }

        public void UpdateEmpresa(EmpresaViewModel empresa)
        {
            Empresa cargoDatabase = _context.Empresa.First(f => f.Id == empresa.Id);
            cargoDatabase.RazonSocial = empresa.RazonSocial;

            _context.SaveChanges();
        }

        public void DeleteEmpresaById(int id)
        {
            _context.Empresa.Remove(_context.Empresa.First(f => f.Id == id));
            _context.SaveChanges();
        }
    }
}