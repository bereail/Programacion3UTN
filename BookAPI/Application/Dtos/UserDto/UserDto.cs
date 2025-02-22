using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDto
{
    public class UserDto
    {
        public string? Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public UserRole Role { get; set; }

        public bool IsActive { get; set; }
    }
}
