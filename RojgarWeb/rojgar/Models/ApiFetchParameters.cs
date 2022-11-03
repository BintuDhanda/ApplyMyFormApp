using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Models
{
    public class ApiFetchParameters
    {
        public Int64 id { get; set; }
        public Int64 id2 { get; set; }
        public List<Int64> listId { get; set; }
        public string stringId { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public DateTime dateFrom { get; set; }

        public DateTime dateTo { get; set; }

        public int days { get; set; }

        public string search { get; set; }

        public int pinCode { get; set; }

        public int listCount { get; set; }
        public int quantity { get; set; }

        public string subCat { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public int Otp { get; set; }
        public string DeviceToken { get; set; }
        public string reason { get; set; }
    }
}
