﻿using Application.Dtos.UserDto;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Entities;
using Domain.Enums;
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

        public User? GetUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public User? GetUserById(int id)
        {
            return _context.Users.Find(id);
        }


        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }


        public ICollection<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }


        public ICollection<User> GetUsersByRole(UserRole role)
        {
            var users = _context.Users.Where(u => (int)u.Role == (int)role).ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"Usuario encontrado: {user.Id} - {user.UserName}");
            }

            return users;
        }


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