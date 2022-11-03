using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class FormFees
    {
        public Int64 Id { get; set; }
        public Int64 JobPostId { get; set; }
        [ForeignKey("JobPostId")]
        public JobPost JobPost { get; set; }
        public string Category { get; set; }
        public decimal FormFee { get; set; }
        public decimal FormFillingFee { get; set; }
        public decimal AdmitCardFee { get; set; }
        public string AddedBy { get; set; }
        [ForeignKey("AddedBy")]
        public ApplicationUser User { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
