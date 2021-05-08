using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class CustomerUser
    {
        public long CustomerID { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string ServiceType { get; } = "Customer";
        public string token { get; set; }
    }
}
