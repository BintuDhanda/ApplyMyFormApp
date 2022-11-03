using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class Category
    {
        [Key]
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string AddedBy { get; set; }
        [ForeignKey("AddedBy")]
        public virtual ApplicationUser User { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
