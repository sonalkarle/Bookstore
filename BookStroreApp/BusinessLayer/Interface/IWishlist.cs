using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public  interface IWishlist
    {
        public ICollection<Customerwishlist> AddBookToWishList(long CustomerID, long BookID);
        ICollection<Customerwishlist> RemoveBookFromWishList(long customerID, long bookID);
        public ICollection<Customerwishlist> GetWishList(long CustomerID);
    }
}
