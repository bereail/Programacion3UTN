using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        { 
            _context = context;
        }
        public ICollection<User> GetAllUsers(string role)
        {
            //para converir el string a Role
            //true ignora mayuscula/minúscula
            if (!Enum.TryParse<UserRole>(role, true, out var parsedRole))
            {
                throw new ArgumentException("El rol especificado no es válido.", nameof(role));
            }

            return _context.Users.Where(u => u.Role == parsedRole).ToList();
        }

        public User? GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }

        public void UpdateUser(User userToUpdate)
        {
            _context.Entry(userToUpdate).State = EntityState.Modified;
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
                _context.Users.Remove(user);
        }

        public ICollection<SaleOrder> GetUserSaleOrders(int userId)
        {
            return _context.Clients
                 .Where(c => c.UserId == userId)
                 .SelectMany(c => c.SaleOrders)
                 .Include(s => s.Book)
                 .ToList();
        }
        public User? ValidateUser(AuthenticationRequestBody authenticationRequestBody)
        {
            return _context.Users.FirstOrDefault(c => c.Email == authenticationRequestBody.Email && c.Password == authenticationRequestBody.Password);
        }


        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public int AddUser(User newUser)
        {
            throw new NotImplementedException();
        }

       

        /* public int AddUser(User newUser)
         {
             _context.Users.Add(newUser);
         }*/
    }
}