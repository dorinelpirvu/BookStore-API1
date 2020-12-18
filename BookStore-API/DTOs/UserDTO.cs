using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_API.DTOs
{
    public class UserDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Dimensiunea trebuie sa fie intre {2} si {1}",MinimumLength = 3)]
        public string Password { get; set; }
        public string Rol { get; set; }
    }
}
