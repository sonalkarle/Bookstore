using BusinessLayer.Interface;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
   public  class WishlistBL : IWishlist
    {
        IWishlistRL customerWishListRL;

        public WishlistBL(IWishlistRL customerWishListRL)
        {
            this.customerWishListRL = customerWishListRL;
        }

        public ICollection<Customerwishlist> GetWishList(long CustomerID)
        {
            try
            {
                var result = customerWishListRL.GetWishList(CustomerID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ICollection<Customerwishlist> AddBookToWishList(long CustomerID, long BookID)
        {
            try
            {
                var result = customerWishListRL.AddBookToWishList(CustomerID, BookID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<Customerwishlist> RemoveBookFromWishList(long CustomerID, long BookID)
        {
            try
            {
                var result = customerWishListRL.RemoveBookFromWishList(CustomerID, BookID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
