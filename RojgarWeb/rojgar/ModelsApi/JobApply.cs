using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.ModelsApi
{
    public class JobApply
    {
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
        public Int64 JobId { get; set; }
        public Int64 PostId { get; set; }
        public Int64 FormFeeId { get; set; }
    }
}
