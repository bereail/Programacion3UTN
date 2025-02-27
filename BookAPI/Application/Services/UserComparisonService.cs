using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserComparisonService :  IUserComparisonService
    {
        private readonly IUserComparisonRepository _userComparisonRepository;

    public UserComparisonService(IUserComparisonRepository userComparisonRepository)
    {
        _userComparisonRepository = userComparisonRepository;
    }

    public bool CompareUserIdWithLoggedInUser(int id, ClaimsPrincipal user)
    {
        return _userComparisonRepository.IsAdminOrSameUser(id, user);
    }
}
}
