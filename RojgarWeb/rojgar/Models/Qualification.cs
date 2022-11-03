using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class Qualification
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string FileUrl { get; set; }
        public string AddedBy { get; set; }
        [ForeignKey("AddedBy")]
        public ApplicationUser User { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UpdatedBy { get; set; }
        [ForeignKey("UpdatedBy")]
        public ApplicationUser UpdateUser { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
