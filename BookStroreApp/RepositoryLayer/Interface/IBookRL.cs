using CommonLayer.RequestModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public  interface IBookRL
    {
        RespnseBook AddBook(RespnseBook book);
        ICollection<RespnseBook> GetCustomerBooks();
        bool DeleteBook(long bookID);
        RespnseBook UpdateBook(long BookID, RespnseBook book);
        ICollection<RespnseBook> GetPriceSortBooks(long CustomerID, bool sort);


    }
}
