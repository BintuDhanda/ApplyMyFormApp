using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class ApplicationHistory
    {
        public Int64 Id { get; set; }
        public Int64 JobId { get; set; }
        [NotMapped]
        public Job Job { get; set; }
        public Int64 JobPostId { get; set; }
        [ForeignKey("JobPostId")]
        public  JobPost JobPost { get; set; }
        public Int64 PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public PaymentHistory PaymentHistory { get; set; }
        public string Category { get; set; }
        [NotMapped]
        public IFormFile AdmitCard { get; set; }
        public string AdmitCardUrl { get; set; }
        public decimal FormFee { get; set; }
        public decimal FormFillingFee { get; set; }
        public decimal AdmitCardFee { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string AddedBy { get; set; }
        [ForeignKey("AddedBy")]
        public ApplicationUser User { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UpdatedBy { get; set; }
        [ForeignKey("UpdatedBy")]
        public ApplicationUser UpdatedUser { get; set; }
        public DateTime UpdatedOn { get; set; }
        [NotMapped]
        public RefundHistory RefundHistory  { get; set; }
    }
}
