using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class CustomAddressResponse
    {
        public string Name { get; set; }
        public long AddressID { get; set; }
        public long CustomerID { get; set; }
        public int Pincode { get; set; }
        public long PhoneNumber { get; set; }
        public string Locality { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Landmark { get; set; }
        public string AddressType { get; set; }
    }
}
