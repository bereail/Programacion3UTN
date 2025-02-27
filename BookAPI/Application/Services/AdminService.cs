/*using Application.Dtos.AdminDTOs;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Models.Entities;
using Infraestructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdminService : IUserServices, IAdminService
    {
        public AdminService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {

        }

        public ICollection<AdminDTO> GetAllAdmins()
        {
            var admins = _userRepository.GetAllUsers("Admin");
            return _mapper.Map<ICollection<AdminDTO>>(admins);
        }
       
        public AdminDTO GetAdminById(int adminId)
        {
            var admin2 = _userRepository.GetUserById(adminId, "Admin");
            return _mapper.Map<AdminDTO>(admin2);
        }

        public AdminDTO AddAdmin(AdminToCreateDTO adminToCreateDTO)
        {
            var newAdmin = _mapper.Map<Admin>(adminToCreateDTO);
            _userRepository.AddUser(newAdmin);
            _userRepository.SaveChanges();
            return _mapper.Map<AdminDTO>(newAdmin);
        }

        public void UpdateAdmin(AdminToUpdateDTO adminToUpdateDTO, int adminId)
        {
            var adminToUpdate = _userRepository.GetUserById(adminId, "Admin");

            _mapper.Map(adminToUpdateDTO, adminToUpdate);
            _userRepository.UpdateUser(adminToUpdate);
            _userRepository.SaveChanges();
        }

    }
}*/