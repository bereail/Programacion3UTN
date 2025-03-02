using Application.Models.Requests;
using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICustomAuthenticationService
    {
        string Login(LoginRequest loginRequest);

        User? ValidateUser(LoginRequest rq);
    }
}
