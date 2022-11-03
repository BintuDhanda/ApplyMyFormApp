using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class PaymentHistory
    {
        public Int64 Id { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string PaymentMode { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public string PaidBy { get; set; }
        [ForeignKey("PaidBy")]
        public virtual ApplicationUser User { get; set; }
        public DateTime TimeStamp { get; set; }
        [NotMapped]
        public string FullName { get; set; }
        [NotMapped]
        public string UserName { get; set; }
    }
}
