using Application.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class UserComparisonRepository : IUserComparisonRepository
    {
        private readonly ApplicationContext _context;

    public UserComparisonRepository(ApplicationContext context)
    {
        _context = context;
    }

    public bool IsAdminOrSameUser(int id, ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int loggedInUserId))
        {
            var loggedInUserRoles = user.FindAll(ClaimTypes.Role).Select(r => r.Value);
            bool isAdmin = loggedInUserRoles.Contains("Admin");

            return isAdmin || id == loggedInUserId;
        }

        return false;
    }
}
}