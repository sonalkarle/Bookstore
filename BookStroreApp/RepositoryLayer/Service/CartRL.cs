using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CartRL : ICartRL
    {
        private static string connectionString = "Data Source=DESKTOP-OKP25QH;Initial Catalog=BookStore;Integrated Security=SSPI";
        private IConfiguration Configuration { get; }
        
        public CartRL(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        public ICollection<CustomerCart> AddBookToCart(long CustomerID, long BookID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {

                        connection.Open();
                        SqlCommand cmd = new SqlCommand("InsertBookToCart", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                        cmd.Parameters.AddWithValue("BookID", BookID);
                        var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        SqlDataReader rd = cmd.ExecuteReader();
                        var result = returnParameter.Value;
                        if (result != null && result.Equals(3))
                        {
                            throw new Exception("Book out of stock");
                        }
                        else if (result != null && result.Equals(2))
                        {
                            throw new Exception("Book don't exist");
                        }
                        ICollection<CustomerCart> cart = new List<CustomerCart>();
                        CustomerCart Book;
                        while (rd.Read())
                        {
                            Book = new CustomerCart();
                            Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                            Book.TotalCost = rd["TotalCost"] == DBNull.Value ? default : rd.GetInt32("TotalCost");
                            Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                            Book.CartID = rd["CartID"] == DBNull.Value ? default : rd.GetInt64("CartID");
                            Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                            Book.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetInt64("CustomerID");
                            Book.Count = rd["BookCount"] == DBNull.Value ? default : rd.GetInt32("BookCount");
                            cart.Add(Book);
                        }
                        return cart;
                    }
                }
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {

                        connection.Open();
                        SqlCommand cmd = new SqlCommand("GetCart", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                        SqlDataReader rd = cmd.ExecuteReader();
                        ICollection<CustomerCart> cart = new List<CustomerCart>();
                        CustomerCart Book;
                        while (rd.Read())
                        {
                            Book = new CustomerCart();
                            Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                            Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                            Book.TotalCost = rd["TotalCost"] == DBNull.Value ? default : rd.GetInt32("TotalCost");
                            Book.CartID = rd["CartID"] == DBNull.Value ? default : rd.GetInt64("CartID");
                            Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                            Book.BookImage = rd["BookImage"] == DBNull.Value ? default : rd.GetString("BookImage");
                            Book.AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName");
                            Book.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetInt64("CustomerID");
                            Book.Count = rd["BookCount"] == DBNull.Value ? default : rd.GetInt32("BookCount");
                            cart.Add(Book);
                        }
                        return cart;
                    }
                }
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("RemoveBookFromCart", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                        cmd.Parameters.AddWithValue("BookID", BookID);
                        var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        SqlDataReader rd = cmd.ExecuteReader();
                        var result = returnParameter.Value;
                        if (result != null && result.Equals(3))
                        {
                            throw new Exception("Book is not in cart");
                        }
                        else if (result != null && result.Equals(2))
                        {
                            throw new Exception("Book don't exist");
                        }
                        ICollection<CustomerCart> cart = new List<CustomerCart>();
                        CustomerCart Book;
                        while (rd.Read())
                        {
                            Book = new CustomerCart();
                            Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                            Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                            Book.TotalCost = rd["TotalCost"] == DBNull.Value ? default : rd.GetInt32("TotalCost");
                            Book.CartID = rd["CartID"] == DBNull.Value ? default : rd.GetInt64("CartID");
                            Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                            Book.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetInt64("CustomerID");
                            Book.Count = rd["BookCount"] == DBNull.Value ? default : rd.GetInt32("BookCount");
                            cart.Add(Book);
                        }
                        return cart;
                    }
                }
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("UpdateBookInCart", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                        cmd.Parameters.AddWithValue("BookID", BookID);
                        cmd.Parameters.AddWithValue("Quantity", Quantity);
                        var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        SqlDataReader rd = cmd.ExecuteReader();
                        var result = returnParameter.Value;
                        if (result != null && result.Equals(3))
                        {
                            throw new Exception("Book out of stock");
                        }
                        else if (result != null && result.Equals(2))
                        {
                            throw new Exception("Book don't exist");
                        }
                        ICollection<CustomerCart> cart = new List<CustomerCart>();
                        CustomerCart Book;
                        while (rd.Read())
                        {
                            Book = new CustomerCart();
                            Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                            Book.TotalCost = rd["TotalCost"] == DBNull.Value ? default : rd.GetInt32("TotalCost");
                            Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                            Book.CartID = rd["CartID"] == DBNull.Value ? default : rd.GetInt64("CartID");
                            Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                            Book.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetInt64("CustomerID");
                            Book.Count = rd["BookCount"] == DBNull.Value ? default : rd.GetInt32("BookCount");
                            Book.AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName");
                            cart.Add(Book);
                        }
                        return cart;
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
