using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class JobPost
    {
        [Key]
        public Int64 JobPostId { get; set; }
        public Int64 JobId { get; set; }
        [ForeignKey("JobId")]
        public Job Job { get; set; }
        public List<FormFees> Fees { get; set; }
        public string JobPostName { get; set; }
        public string JobPostGender { get; set; }
        public string NumberOfPost { get; set; }
        public string AddedBy { get; set; }
        [ForeignKey("AddedBy")]
        public ApplicationUser User { get; set; }
        public DateTime TimeStamp { get; set; }

        [NotMapped]
        public bool IsApplied { get; set; }
    }
}
