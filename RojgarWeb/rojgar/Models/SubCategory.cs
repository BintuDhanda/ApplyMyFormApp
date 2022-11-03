using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class SubCategory
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64 CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string AddedBy { get; set; }
        [ForeignKey("AddedBy")]
        public virtual ApplicationUser User { get; set; }
        public string UpdatedBy { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual ApplicationUser UpdatedUser { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public int Order { get; set; }
    }
}
