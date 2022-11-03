using System.Collections.Generic;

namespace rojgar.Models
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }
        public bool IsSuccess { get; set; }
    }
}
