using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace rojgar.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Identify { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string ImageUrl { get; set; }
        public string DeviceToken { get; set; }
        public bool IsActive { get; set; }
        public DateTime TimeStamp { get; set; }
        [NotMapped]
        public string OldPassword { get; set; }
        [NotMapped]
        public string NewPassword { get; set; }
    }
}
