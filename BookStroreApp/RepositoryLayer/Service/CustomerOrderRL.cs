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
   public  class CustomerOrderRL : ICustomerOrderRL
    {
        private static string connectionString = "Data Source=DESKTOP-OKP25QH;Initial Catalog=BookStore;Integrated Security=SSPI";
        private IConfiguration Configuration { get; }

        public CustomerOrderRL(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        public CustomerOrder PlaceOrder(long CustomerID, long AddressID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("PlaceOrder", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                        cmd.Parameters.AddWithValue("AddressID", AddressID);
                        var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        SqlDataReader rd = cmd.ExecuteReader();
                        var result = returnParameter.Value;
                        if (result != null && result.Equals(0))
                        {
                            throw new Exception("Order failed cart is empty");
                        }
                        CustomerOrder order = new CustomerOrder();
                        if (rd.Read())
                        {
                            order.OrderID = rd["OrderID"] == DBNull.Value ? default : rd.GetInt64("OrderID");
                            order.OrderDate = rd["OrderDate"] == DBNull.Value ? default : rd.GetDateTime("OrderDate");
                            order.TotalCost = rd["TotalCost"] == DBNull.Value ? default : rd.GetInt32("TotalCost");
                            order.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetInt64("CustomerID");
                            order.Email = rd["Email"] == DBNull.Value ? default : rd.GetString("Email");
                            order.AddressID = rd["OrderAddressID"] == DBNull.Value ? default : rd.GetInt64("OrderAddressID");
                            order.PhoneNumber = rd["PhoneNumber"] == DBNull.Value ? default : rd.GetInt64("PhoneNumber");
                            order.Address = rd["Address"] == DBNull.Value ? default : rd.GetString("Address");
                            order.Name = rd["Name"] == DBNull.Value ? default : rd.GetString("Name");
                            order.AddressType = rd["AddressType"] == DBNull.Value ? default : rd.GetString("AddressType");
                            order.City = rd["City"] == DBNull.Value ? default : rd.GetString("City");
                            order.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetInt64("CustomerID");
                            order.Pincode = rd["Pincode"] == DBNull.Value ? default : rd.GetInt32("Pincode");
                            order.Landmark = rd["Landmark"] == DBNull.Value ? default : rd.GetString("Landmark");
                            order.Locality = rd["Locality"] == DBNull.Value ? default : rd.GetString("Locality");

                        }
                        return order;
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
