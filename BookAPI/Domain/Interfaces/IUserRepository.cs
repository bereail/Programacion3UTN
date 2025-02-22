using Domain.Models;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository
    {
        ICollection<User> GetAllUsers(string role);
        User? GetUserById(int userId);
        User? GetUserByUsername(string username);
        int AddUser(User newUser);
        void UpdateUser(User userToUpdate);
        void DeleteUser(int userId);
        User? ValidateUser(AuthenticationRequestBody authenticationRequestBody);
    }
}