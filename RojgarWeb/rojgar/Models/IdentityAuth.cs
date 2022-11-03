using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class IdentityAuth
    {
        [Key]
        public int Id { get; set; }
        public int Otp { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public string Token { get; set; }
        public bool IsPhone { get; set; }
        public int MaxAttempt { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
