using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityCode { get; set; }
        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public virtual State State { get; set; }
        [NotMapped]
        public string StateName { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public string AddedBy { get; set; }
        [ForeignKey("AddedBy")]
        public virtual ApplicationUser UserAddedBy { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UpdatedBy { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual ApplicationUser UserUpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; }
    }
}
