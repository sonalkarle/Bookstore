﻿using CommonLayer.RequestModel;
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
    public class CustomerAddressRL : ICustomerAddressRL
    {
        private static string connectionString = "Data Source=DESKTOP-OKP25QH;Initial Catalog=BookStore;Integrated Security=SSPI";
        private IConfiguration Configuration { get; }
        
        public CustomerAddressRL(IConfiguration configuration)
        {

            Configuration = configuration;
        }
        public CustomAddressResponse AddCustomerAddress(CustomerAddress address)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    CustomAddressResponse customerAddress = new CustomAddressResponse();
                    using (connection)
                    {

                        connection.Open();
                        SqlCommand cmd = new SqlCommand("InsertCustomerAddress", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("CustomerID", address.CustomerID);
                        cmd.Parameters.AddWithValue("Name", address.Name);
                        cmd.Parameters.AddWithValue("Pincode", address.Pincode);
                        cmd.Parameters.AddWithValue("PhoneNumber", address.PhoneNumber);
                        cmd.Parameters.AddWithValue("Address", address.Address);
                        cmd.Parameters.AddWithValue("City", address.City);
                        cmd.Parameters.AddWithValue("AddressType", address.AddressType);
                        cmd.Parameters.AddWithValue("Landmark", address.Landmark);
                        cmd.Parameters.AddWithValue("Locality", address.Locality);
                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.Read())
                        {
                            customerAddress.AddressID = rd["CustomerAddressID"] == DBNull.Value ? default : rd.GetInt64("CustomerAddressID");
                            customerAddress.PhoneNumber = rd["PhoneNumber"] == DBNull.Value ? default : rd.GetInt64("PhoneNumber");
                            customerAddress.Address = rd["Address"] == DBNull.Value ? default : rd.GetString("Address");
                            customerAddress.Name = rd["Name"] == DBNull.Value ? default : rd.GetString("Name");
                            customerAddress.AddressType = rd["AddressType"] == DBNull.Value ? default : rd.GetString("AddressType");
                            customerAddress.City = rd["City"] == DBNull.Value ? default : rd.GetString("City");
                            customerAddress.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetInt64("CustomerID");
                            customerAddress.Pincode = rd["Pincode"] == DBNull.Value ? default : rd.GetInt32("Pincode");
                            customerAddress.Landmark = rd["Landmark"] == DBNull.Value ? default : rd.GetString("Landmark");
                            customerAddress.Locality = rd["Locality"] == DBNull.Value ? default : rd.GetString("Locality");
                        }
                        return customerAddress;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

            public bool DeleteCustomerAddress(long customerID, long addressID)
            {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (connection)
                    {

                        connection.Open();
                        SqlCommand cmd = new SqlCommand("DeleteCustomerAddress", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("CustomerID", customerID);
                        cmd.Parameters.AddWithValue("AddressID", addressID);
                        var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteReader();
                        var result = returnParameter.Value;
                        if (result != null && result.Equals(2))
                        {
                            throw new Exception("AddressID dont exist");
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

            public ICollection<CustomAddressResponse> GetAllCustomerAddress(long customerID)
            {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (connection)
                    {
                        ICollection<CustomAddressResponse> customerAddresses = new List<CustomAddressResponse>();
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("GetAllCustomerAddress", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("CustomerID", customerID);
                        var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        SqlDataReader rd = cmd.ExecuteReader();
                        var result = returnParameter.Value;
                        while (rd.Read())
                        {
                            CustomAddressResponse customerAddress = new CustomAddressResponse();
                            customerAddress.AddressID = rd["CustomerAddressID"] == DBNull.Value ? default : rd.GetInt64("CustomerAddressID");
                            customerAddress.PhoneNumber = rd["PhoneNumber"] == DBNull.Value ? default : rd.GetInt64("PhoneNumber");
                            customerAddress.Address = rd["Address"] == DBNull.Value ? default : rd.GetString("Address");
                            customerAddress.Name = rd["Name"] == DBNull.Value ? default : rd.GetString("Name");
                            customerAddress.AddressType = rd["AddressType"] == DBNull.Value ? default : rd.GetString("AddressType");
                            customerAddress.City = rd["City"] == DBNull.Value ? default : rd.GetString("City");
                            customerAddress.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetInt64("CustomerID");
                            customerAddress.Pincode = rd["Pincode"] == DBNull.Value ? default : rd.GetInt32("Pincode");
                            customerAddress.Landmark = rd["Landmark"] == DBNull.Value ? default : rd.GetString("Landmark");
                            customerAddress.Locality = rd["Locality"] == DBNull.Value ? default : rd.GetString("Locality");
                            customerAddresses.Add(customerAddress);
                        }
                        return customerAddresses;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

            public CustomAddressResponse UpdateCustomerAddress(CustomerAddress address)
            {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (connection)
                    {
                        CustomAddressResponse customerAddress = new CustomAddressResponse();
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("UpdateCustomerAddress", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("CustomerID", address.CustomerID);
                        cmd.Parameters.AddWithValue("CustomerAddressID", address.CustomerAddressID);
                        cmd.Parameters.AddWithValue("Name", address.Name);
                        cmd.Parameters.AddWithValue("Pincode", address.Pincode);
                        cmd.Parameters.AddWithValue("PhoneNumber", address.PhoneNumber);
                        cmd.Parameters.AddWithValue("Address", address.Address);
                        cmd.Parameters.AddWithValue("City", address.City);
                        cmd.Parameters.AddWithValue("AddressType", address.AddressType);
                        cmd.Parameters.AddWithValue("Landmark", address.Landmark);
                        cmd.Parameters.AddWithValue("Locality", address.Locality);
                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.Read())
                        {
                            customerAddress.AddressID = rd["CustomerAddressID"] == DBNull.Value ? default : rd.GetInt64("CustomerAddressID");
                            customerAddress.PhoneNumber = rd["PhoneNumber"] == DBNull.Value ? default : rd.GetInt64("PhoneNumber");
                            customerAddress.Address = rd["Address"] == DBNull.Value ? default : rd.GetString("Address");
                            customerAddress.Name = rd["Name"] == DBNull.Value ? default : rd.GetString("Name");
                            customerAddress.AddressType = rd["AddressType"] == DBNull.Value ? default : rd.GetString("AddressType");
                            customerAddress.City = rd["City"] == DBNull.Value ? default : rd.GetString("City");
                            customerAddress.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetInt64("CustomerID");
                            customerAddress.Pincode = rd["Pincode"] == DBNull.Value ? default : rd.GetInt32("Pincode");
                            customerAddress.Landmark = rd["Landmark"] == DBNull.Value ? default : rd.GetString("Landmark");
                            customerAddress.Locality = rd["Locality"] == DBNull.Value ? default : rd.GetString("Locality");
                        }
                        return customerAddress;
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
