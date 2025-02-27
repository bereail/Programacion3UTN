using Application.Dtos.AdminDTOs;
using Infraestructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAdminService : IUserService
    {
        AdminDTO GetAdminById(int adminId);
        public ICollection<AdminDTO> GetAllAdmins();
        public AdminDTO AddAdmin(AdminToCreateDTO adminToCreateDTO);
        public void UpdateAdmin(AdminToUpdateDTO adminToUpdateDTO, int adminId);

    }
}