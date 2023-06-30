using Model.DTO;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IEmpresaService
    {
        List<EmpresaDTO> GetAll();
        EmpresaDTO InsertEmpresa(EmpresaViewModel cargo);
        void UpdateEmpresa(EmpresaViewModel cargo);
        void DeleteEmpresaById(int id);
    }
}
