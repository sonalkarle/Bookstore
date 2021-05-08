using CommonLayer.RequestModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
   public  class BookRL : IBookRL
    {

        private static string connectionString = "Data Source=DESKTOP-OKP25QH;Initial Catalog=BookStore;Integrated Security=SSPI";
        private IConfiguration Configuration { get; }
        RespnseBook Book;
        public BookRL(IConfiguration configuration)
        {

            Configuration = configuration;
        }
        public RespnseBook AddBook(RespnseBook book)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {

                        connection.Open();
                        SqlCommand cmd = new SqlCommand("InserBookRecord", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("BookName", book.BookName);
                        cmd.Parameters.AddWithValue("BookDiscription", book.BookDiscription);
                        cmd.Parameters.AddWithValue("BookImage", book.BookImage);
                        cmd.Parameters.AddWithValue("BookPrice", book.BookPrice);
                        cmd.Parameters.AddWithValue("BookQuantity", book.Quantity);
                        cmd.Parameters.AddWithValue("AuthorName", book.AuthorName);
                        SqlDataReader rd = cmd.ExecuteReader();
                        RespnseBook Book = new RespnseBook();
                        if (rd.Read())
                        {
                          
                            Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                            Book.BookDiscription = rd["BookDiscription"] == DBNull.Value ? default : rd.GetString("BookDiscription");
                            Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                            Book.Quantity = rd["BookQuantity"] == DBNull.Value ? default : rd.GetInt32("BookQuantity");
                            Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                            Book.AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName");
                            Book.BookImage = rd["BookImage"] == DBNull.Value ? default : rd.GetString("BookImage");
                            Book.InStock = rd["InStock"] != DBNull.Value && rd.GetBoolean("InStock");
                        }
                        return Book;
                    }
                }
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        ICollection<RespnseBook> Books = new List<RespnseBook>();

                        connection.Open();

                        SqlCommand cmd = new SqlCommand("DeleteBookRecord", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("BookID", bookID);
                        var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();
                        var result = returnParameter.Value;
                        if (result.Equals(2))
                        {
                            throw new Exception("book don't exist");
                        }
                        else if (result.Equals(3))
                        {
                            throw new Exception("book already deleted");
                        }
                        return true;
                    }
                }
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
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        using (connection)
                        {
                            List<RespnseBook> Books = new List<RespnseBook>();
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("GetBookRecord", connection)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            
                            SqlDataReader rd = cmd.ExecuteReader();
                            while (rd.Read())
                            {
                                RespnseBook Book = new RespnseBook();
                                Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                                Book.BookDiscription = rd["BookDiscription"] == DBNull.Value ? default : rd.GetString("BookDiscription");
                                Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                                Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                                Book.AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName");
                                Book.BookImage = rd["BookImage"] == DBNull.Value ? default : rd.GetString("BookImage");
                                Book.InStock = rd["InStock"] == DBNull.Value ? default : rd.GetBoolean("InStock");
                                Books.Add(Book);
                            }
                            return Books;
                        }
                    }
                }
            }


            catch (Exception)
            {

                throw;
            }
          
        }
        public ICollection<RespnseBook> GetPriceSortBooks(long CustomerID,bool sort)
        {
            try
            {
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        using (connection)
                        {
                            List<RespnseBook> Books = new List<RespnseBook>();
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("GetPriceSortedBooks", connection)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                            cmd.Parameters.AddWithValue("order", sort);
                            SqlDataReader rd = cmd.ExecuteReader();
                            while (rd.Read())
                            {
                                RespnseBook Book = new RespnseBook();
                                Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                                Book.BookDiscription = rd["BookDiscription"] == DBNull.Value ? default : rd.GetString("BookDiscription");
                                Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                                Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                                Book.AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName");
                                Book.BookImage = rd["BookImage"] == DBNull.Value ? default : rd.GetString("BookImage");
                                Book.InStock = rd["InStock"] == DBNull.Value ? default : rd.GetBoolean("InStock");
                                Books.Add(Book);
                            }
                            return Books;
                        }
                    }
                }
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

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                   

                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("UpdateBookRecord", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("BookID", BookID);
                        cmd.Parameters.AddWithValue("BookName", book.BookName);
                        cmd.Parameters.AddWithValue("BookDiscription", book.BookDiscription);
                        cmd.Parameters.AddWithValue("BookImage", "book");
                        cmd.Parameters.AddWithValue("BookPrice", book.BookPrice);
                        cmd.Parameters.AddWithValue("BookQuantity", book.Quantity);
                        cmd.Parameters.AddWithValue("AuthorName", book.AuthorName);
                        var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        SqlDataReader rd = cmd.ExecuteReader();
                        var result = returnParameter.Value;
                        if (result != null && result.Equals(2))
                            throw new Exception("book don't exist");
                        if (rd.Read())
                        {
                            Book = new RespnseBook
                            {
                                BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID"),
                                BookDiscription = rd["BookDiscription"] == DBNull.Value ? default : rd.GetString("BookDiscription"),
                                BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice"),
                                BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName"),
                                AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName"),
                                BookImage = rd["BookImage"] == DBNull.Value ? default : rd.GetString("BookImage"),
                                InStock = rd["InStock"] != DBNull.Value && rd.GetBoolean("InStock")
                            };
                            
                        }
                        return Book;

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }


    }
}
