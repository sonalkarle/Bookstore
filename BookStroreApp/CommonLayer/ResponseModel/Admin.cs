using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class Admin
    {
        public long AdminID { get; set; }
        public string AdminName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ServiceType { get; } = "Admin";
       public string token { get; set; }
    }
}
