using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class Login
    {
        [Required(ErrorMessage = "User Name is Required")]
        public string UserName{ get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool isPersistent { get; set; }
    }
}
