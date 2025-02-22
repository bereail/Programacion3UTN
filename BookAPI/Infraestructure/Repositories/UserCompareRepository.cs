using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class UserCompareRepository : IUserComparisonRepository
    {
        private readonly ApplicationContext _context;

        public UserCompareRepository(ApplicationContext context)
        {
            _context = context;
        }

        //-------------------------------------------------------------------------------------------------------------------------
        public bool CompareUserIdWithLoggedInUser(int id, ClaimsPrincipal user)
        {

            var userIdClaim = user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");


            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int loggedInUserId))
            {
                var loggedInUserRoles = user.FindAll("http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(r => r.Value);

                bool isAdmin = loggedInUserRoles.Contains("Admin");

                Console.WriteLine($"User ID in Claims: {loggedInUserId}, ID to compare: {id}");


                return isAdmin || (id == loggedInUserId);
            }

            return false;
        }
    }
}
