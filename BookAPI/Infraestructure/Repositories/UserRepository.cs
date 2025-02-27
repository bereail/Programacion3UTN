using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Enums;
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
    public class UserRepository : Repository, IUserRepository
    {

        public UserRepository(ApplicationContext context) : base(context)
        {
        }

        //OK
        public User? GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        //OK
        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        //OK
        public ICollection<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        //OK
        public ICollection<User> GetUsersByRole(UserRole role)
        {
            var users = _context.Users.Where(u => (int)u.Role == (int)role).ToList();

            Console.WriteLine($"Buscando usuarios con rol: {role} ({(int)role})");
            Console.WriteLine($"Usuarios encontrados: {users.Count}");

            foreach (var user in users)
            {
                Console.WriteLine($"Usuario encontrado: {user.Id} - {user.UserName}");
            }

            return users;
        }


        //OK
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public List<int> GetBookingIdsByUserId(int userId)
        {
            return _context.SaleOrders
                .Where(so => so.ClientId == userId)
                .Select(so => so.SaleOrderId)
                .ToList();
        }



        public int AddUser(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser.Id;
        }

        public User? ValidateUser(AuthenticationRequestBody authenticationRequestBody)
        {
            return _context.Users.FirstOrDefault(u =>
                u.Email == authenticationRequestBody.Email &&
                u.Password == authenticationRequestBody.Password);
        }


    }
}