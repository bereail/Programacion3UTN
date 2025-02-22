using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDto
{
    public class UserForUpdateRequest
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string Email { get; set; }
        //Requerido
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }


    }
}
