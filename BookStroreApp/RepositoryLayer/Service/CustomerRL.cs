using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CustomerRL: ICustomerRL
    {

        private static string connectionString = "Data Source=DESKTOP-OKP25QH;Initial Catalog=BookStore;Integrated Security=SSPI";
        private IConfiguration Configuration { get; }
        public CustomerRL(IConfiguration configuration)
        {

            Configuration = configuration;


        }


        public CustomerUser RegisterDetails( RegisterCustomer registration)
        {
            try
            {

                CustomerUser customerUser = new CustomerUser();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        connection.Open();
                        //define the SqlCommand Object
                        SqlCommand cmd = new SqlCommand("spRegisterCustomerDetails", connection);
                        cmd.CommandType = CommandType.StoredProcedure;




                        cmd.Parameters.AddWithValue("@FirstName", registration.CustomerFirstName);
                        cmd.Parameters.AddWithValue("@LastName", registration.CustomerLastName);
                        cmd.Parameters.AddWithValue("@PhoneNumber", registration.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Email", registration.Email);
                        cmd.Parameters.AddWithValue("@Password", Password.ConvertToEncrypt(registration.Password));

                        SqlDataReader sqlDataReader = cmd.ExecuteReader();


                        if (sqlDataReader.HasRows)
                        {
                            if (sqlDataReader.Read())
                            {
                                customerUser.CustomerID = sqlDataReader.GetInt64(0);
                                customerUser.CustomerFirstName = sqlDataReader.GetString(1);
                                customerUser.CustomerLastName = sqlDataReader.GetString(2);
                                customerUser.Email = sqlDataReader.GetString(3);
                                customerUser.PhoneNumber = sqlDataReader.GetInt64(4);
                            }
                        }

                        return customerUser;

                        //Close Data Reader
                        sqlDataReader.Close();
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public CustomerUser LoginCustome(LoginCustomer loginCustomerAccount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {

                        connection.Open();
                        SqlCommand cmd = new SqlCommand("FetchCustomerRecord", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("Email", loginCustomerAccount.Email);
                        cmd.Parameters.AddWithValue("Password", loginCustomerAccount.Password);
                        var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        CustomerUser customer = new CustomerUser();
                        SqlDataReader rd = cmd.ExecuteReader();
                        var result = returnParameter.Value;

                        if (result != null && result.Equals(2))
                        {
                            throw new Exception("Email not registered");
                        }
                        if (result != null && result.Equals(3))
                        {
                            throw new Exception("wrong password");
                        }
                        if (rd.Read())
                        {
                            customer.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetInt64("CustomerID");
                            customer.CustomerFirstName = rd["CustomerFirstName"] == DBNull.Value ? default : rd.GetString("CustomerFirstName");
                            customer.CustomerLastName = rd["CustomerLastName"] == DBNull.Value ? default : rd.GetString("CustomerLastName");
                            customer.Email = rd["Email"] == DBNull.Value ? default : rd.GetString("Email");
                            customer.PhoneNumber = rd["PhoneNumber"] == DBNull.Value ? default : rd.GetInt64("PhoneNumber");
                        }
                        return customer;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
       }







        public string CreateToken(CustomerUser info)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, "User"),

                            new Claim("Email", info.Email.ToString() ),

                            new Claim("CustomerID", info.CustomerID.ToString()),

                        };

                var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
                  Configuration["Jwt:Issuer"],
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MSMQModel ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
                CustomerUser customerUser = new CustomerUser();
                LoginAdmin forget1 = new LoginAdmin();
                JwtModel forget2 = new JwtModel();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        connection.Open();
                        //define the SqlCommand Object
                        SqlCommand cmd = new SqlCommand("spForgetPassword", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Email", forgetPasswordModel.Email);
                        SqlDataReader sqlDataReader = cmd.ExecuteReader();


                        if (sqlDataReader.HasRows)
                        {
                            if (sqlDataReader.Read())
                            {

                                customerUser.Email = sqlDataReader.GetString(0);
                            }
                            var jwt = CreateToken(customerUser);
                            forget2.JwtToken = jwt;

                            var model1 = new ForgetPasswordModel { Email = forgetPasswordModel.Email };
                            var model2 = new JwtModel { JwtToken = forget2.JwtToken };
                            var model = new MSMQModel { Email = model1.Email, JwtToken = model2.JwtToken };
                            return model;
                        }

                        else
                        {
                            return null;
                        }

                        //Close Data Reader
                        sqlDataReader.Close();
                        connection.Close();
                    }
                }



            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public bool ResetCustomerAccountPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("ChangeCustomerPassword", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("Email", resetPasswordModel.Email);
                        cmd.Parameters.AddWithValue("NewPassword",Password.ConvertToEncrypt( resetPasswordModel.NewPassword));
                        var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        CustomerUser customer = new CustomerUser();
                        SqlDataReader rd = cmd.ExecuteReader();
                        var result = returnParameter.Value;

                        if (result != null && result.Equals(1))
                        {
                            return true;
                        }
                        return false;
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
