using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Services
{
    public class UserComparisonService : IUserComparisonRepository
    {
        public bool CompareUserIdWithLoggedInUser(int id, ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int loggedInUserId))
            {
                var loggedInUserRoles = user.FindAll(ClaimTypes.Role).Select(r => r.Value);
                bool isAdmin = loggedInUserRoles.Contains("Admin");

                Console.WriteLine($"User ID in Claims: {loggedInUserId}, ID to compare: {id}");

                return isAdmin || (id == loggedInUserId);
            }

            return false;
        }
    }
}