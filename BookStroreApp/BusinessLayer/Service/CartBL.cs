using BusinessLayer.Interface;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
   public class CartBL : ICartBL
    {
        ICartRL customerCartRL;

        public CartBL(ICartRL customerCartRL)
        {
            this.customerCartRL = customerCartRL;
        }

        public ICollection<CustomerCart> AddBookToCart(long CustomerID, long BookID)
        {
            try
            {
                var result = customerCartRL.AddBookToCart(CustomerID, BookID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ICollection<CustomerCart> UpdateBookInCart(long CustomerID, long BookID, long Quantity)
        {
            try
            {
                ICollection<CustomerCart> result;
                result = customerCartRL.UpdateBookInCart(CustomerID, BookID, Quantity);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<CustomerCart> GetCart(long CustomerID)
        {
            try
            {
                var result = customerCartRL.GetCart(CustomerID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<CustomerCart> RemoveBookFromCart(long CustomerID, long BookID)
        {
            try
            {
                ICollection<CustomerCart> result = customerCartRL.RemoveBookFromCart(CustomerID, BookID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
