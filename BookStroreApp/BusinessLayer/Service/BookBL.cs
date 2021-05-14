using BusinessLayer.Interface;
using CommonLayer.RequestModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class BookBL : IBookBL
    {
        IBookRL bookManagementRL;
         CloudinaryBL cloudinary;

        RespnseBook responseBook;

        public BookBL(IBookRL bookManagementRL, IConfiguration config)
        {
            cloudinary = new CloudinaryBL(config);
            this.bookManagementRL = bookManagementRL;
        }

        public RespnseBook AddBook(RequestBook book)
        {
            try
            {
                string ImageUrl = "";
                if (book.BookImage != null)
               {
                   ImageUrl = cloudinary.UploadImage(book.BookName, book.BookImage);
               }

                responseBook = new RespnseBook
                {
                    BookName = book.BookName,
                    BookDiscription = book.BookDiscription == null ? "" : book.BookDiscription,
                    BookPrice = book.BookPrice,
                    BookImage = ImageUrl,
                    AuthorName = book.AuthorName,
                    Quantity = book.Quantity
                };

                var result = bookManagementRL.AddBook(responseBook);
                responseBook = new RespnseBook
                {
                    BookID = result.BookID,
                    BookDiscription = result.BookDiscription,
                    BookImage = result.BookImage,
                    BookName = result.BookName,
                    BookPrice = result.BookPrice,
                    AuthorName = result.AuthorName,
                    InCart = result.InCart,
                    InStock = result.InStock,
                    Quantity = result.Quantity
                };
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteBook(long bookID)
        {
            try
            {
                return bookManagementRL.DeleteBook(bookID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ICollection<RespnseBook> GetCustomerBooks()
        {
            try
            {
                return bookManagementRL.GetCustomerBooks();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<RespnseBook> GetCustomersortBooks(long CustomerID,bool sort)
        {
            try
            {
                return bookManagementRL.GetPriceSortBooks(CustomerID,sort);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RespnseBook UpdateBook(long BookID, RespnseBook book)
        {
            try
            {
                return bookManagementRL.UpdateBook(BookID, book);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
