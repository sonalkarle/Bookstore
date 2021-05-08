using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public  interface IWishlistRL
    {
        public ICollection<Customerwishlist> AddBookToWishList(long CustomerID, long BookID);
        ICollection<Customerwishlist> RemoveBookFromWishList(long customerID, long bookID);
        public ICollection<Customerwishlist> GetWishList(long CustomerID);
    }
}
