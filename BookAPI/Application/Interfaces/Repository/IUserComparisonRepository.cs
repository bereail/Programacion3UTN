using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IUserComparisonRepository
    {
        //Funciòn para comprobar si el usuario autenticado coincide con el id ingresado o si es admin
        bool IsAdminOrSameUser(int id, ClaimsPrincipal user);
    }
}
