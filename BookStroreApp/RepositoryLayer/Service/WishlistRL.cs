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
   public  class WishlistRL : IWishlistRL
    {
        private static string connectionString = "Data Source=DESKTOP-OKP25QH;Initial Catalog=BookStore;Integrated Security=SSPI";
        private IConfiguration Configuration { get; }
       
        public WishlistRL(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        public ICollection<Customerwishlist> GetWishList(long CustomerID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {

                        connection.Open();
                        SqlCommand cmd = new SqlCommand("GetWishList", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                        SqlDataReader rd = cmd.ExecuteReader();
                        ICollection<Customerwishlist> WishList = new List<Customerwishlist>();
                        Customerwishlist Book;
                        while (rd.Read())
                        {
                            Book = new Customerwishlist();
                            Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                            Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                            Book.WishListID = rd["WishListID"] == DBNull.Value ? default : rd.GetInt64("WishListID");
                            Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                            Book.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetInt64("CustomerID");
                            WishList.Add(Book);
                        }
                        return WishList;
                    }
                }
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("InsertBookToWishList", connection)
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
                            throw new Exception("Book already in wish list");
                        }
                        else if (result != null && result.Equals(2))
                        {
                            throw new Exception("Book don't exist");
                        }
                        ICollection<Customerwishlist> WishList = new List<Customerwishlist>();
                        Customerwishlist Book;
                        while (rd.Read())
                        {
                            Book = new Customerwishlist();
                            Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                            Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                            Book.WishListID = rd["WishListID"] == DBNull.Value ? default : rd.GetInt64("WishListID");
                            Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                            Book.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetInt64("CustomerID");
                            WishList.Add(Book);
                        }
                        return WishList;
                    }
                }
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("RemoveBookFromWishList", connection)
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
                            throw new Exception("Book is not in wish list");
                        }
                        else if (result != null && result.Equals(2))
                        {
                            throw new Exception("Book don't exist");
                        }
                        ICollection<Customerwishlist> WishList = new List<Customerwishlist>();
                        Customerwishlist Book;
                        while (rd.Read())
                        {
                            Book = new Customerwishlist();
                            Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                            Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                            Book.WishListID = rd["WishListID"] == DBNull.Value ? default : rd.GetInt64("WishListID");
                            Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                            Book.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetInt64("CustomerID");
                            WishList.Add(Book);
                        }
                        return WishList;
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
