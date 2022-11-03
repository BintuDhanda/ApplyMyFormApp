using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class Job
    {
        public Int64 JobId { get; set; }
        public Int64 CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public string JobName { get; set; }
        public string JobDepartment { get; set; }
        public string AdvtNumber { get; set; }
        public DateTime JobPublishDate { get; set; }
        public DateTime JobStartDate { get; set; }
        public DateTime JobEndDate { get; set; }
        public string JobDescription { get; set; }
        public string QualificationLevel { get; set; }
        [NotMapped]
        public IFormFile JobPdf { get; set; }
        public string JobPdfUrl { get; set; }
        public bool IsActive { get; set; }
        public string AddedBy { get; set; }
        [ForeignKey("AddedBy")]
        public ApplicationUser User { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
