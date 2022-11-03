using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class RefundHistory
    {
        public Int64 Id { get; set; }
        public Int64 PaymentHistoryId { get; set; }
        public virtual PaymentHistory PaymentHistory { get; set; }
        public Int64 ApplicationHistoryId { get; set; }
        public virtual ApplicationHistory ApplicationHistory { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
    }
}
