using Application.Interfaces.Services;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IAuthenticationRepository 
    {
        User? GetUserByEmail(string email); 
    }
}
