using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class PushNotification
    {
        public Int64 Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string AddedBy { get; set; }
        [ForeignKey("AddedBy")]
        public virtual ApplicationUser User { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
