namespace rojgar.Models
{

    public class UserLogin
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DeviceToken { get; set; }
        public bool IsExist { get; set; }
        public bool IsOtp { get; set; }
        public int Otp { get; set; }
    }
}
