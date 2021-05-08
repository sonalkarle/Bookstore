using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        public ICollection<CustomerCart> AddBookToCart(long CustomerID, long BookID);
             ICollection<CustomerCart> GetCart(long customerID);
        ICollection<CustomerCart> RemoveBookFromCart(long customerID, long bookID);
        ICollection<CustomerCart> UpdateBookInCart(long customerID, long bookID, long Quantity);
    }
}
