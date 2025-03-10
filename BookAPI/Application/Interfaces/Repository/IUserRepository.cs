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


        //OK
        User? GetUserById(int id);

        //OK
        User? GetUserByEmail(string email);

        //OK
        ICollection<User> GetAllUsers();

        //OK
        ICollection<User> GetUsersByRole(UserRole role);

        //OK 
        void UpdateUser(User user);

        List<int> GetBookingIdsByUserId(int userId);

        int AddUser(User newUser);
        User? ValidateUser(AuthenticationRequestBody authenticationRequestBody);

      /*  public BaseResponse ReactivateUserByCredentials(string email, string password);*/

    }
}
