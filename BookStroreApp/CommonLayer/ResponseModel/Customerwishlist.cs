using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class Customerwishlist
    {
        public long WishListID { get; set; }
        public string BookName { get; set; }
        public long CustomerID { get; set; }
        public long BookID { get; set; }
        public int BookPrice { get; set; }
    }
}
