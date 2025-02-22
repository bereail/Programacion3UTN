using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserComparisonRepository
    {
        //Funciòn para comprobar si el usuario autenticado coincide con el id ingresado o si es admin
        public bool CompareUserIdWithLoggedInUser(int id, ClaimsPrincipal user);
    }
}
