using Model.DTO;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface ICargoService
    {
        List<CargoDTO> GetAll();
        CargoDTO InsertCargo(CargoViewModel cargo);
        void UpdateCargo(CargoViewModel cargo);
        void DeleteCargoById(int id);
    }
}
