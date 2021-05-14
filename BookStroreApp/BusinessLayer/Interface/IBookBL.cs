using CommonLayer.RequestModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public  interface IBookBL
    {
        RespnseBook AddBook(RequestBook book);
        ICollection<RespnseBook> GetCustomerBooks();
        bool DeleteBook(long bookID);
        RespnseBook UpdateBook(long BookID, RespnseBook book);
        ICollection<RespnseBook> GetCustomersortBooks(long CustomerID, bool sort);

    }
}
