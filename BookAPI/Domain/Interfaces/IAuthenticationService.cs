using Domain.Models;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthenticationService
    {
        //Validar si el usuario es nulo o si esta desactivado
        User? ValidateUser(AuthenticationRequestBody authenticationRequestBody);
    }
}
