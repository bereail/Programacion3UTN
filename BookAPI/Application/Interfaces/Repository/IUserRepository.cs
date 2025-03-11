using Application.Dtos;
using Application.Dtos.UserDto;
using Domain.Entities;
using Domain.Entities.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IUserRepository : IRepository
    {
        public User? GetUser(string email, string password);

        User? GetUserById(int id);

        User? GetUserByEmail(string email);

        ICollection<User> GetAllUsers();


        ICollection<User> GetUsersByRole(UserRole role);

        void UpdateUser(User user);

        List<int> GetBookingIdsByUserId(int userId);

        int AddUser(User newUser);
        User? ValidateUser(AuthenticationRequestBody authenticationRequestBody);

    }
}
