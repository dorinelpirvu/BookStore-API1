using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_UI.Models
{
    public class RegistrationModel
    {

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Dimensiunea trebuie sa fie intre {2} si {1}", MinimumLength = 3)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Confirmare parola")]
        [StringLength(50, ErrorMessage = "Dimensiunea trebuie sa fie intre {2} si {1}", MinimumLength = 3)]
        [Compare("Password",ErrorMessage ="Parola nu se potriveste")]
        public string ConfirmPassword { get; set; }
        public string Rol { get; set; }

    }
    public class LoginModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class Logout
    {

    }
}
