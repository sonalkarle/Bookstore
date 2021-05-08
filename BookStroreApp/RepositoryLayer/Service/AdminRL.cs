using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.IbookRL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.BookRL
{
     public class AdminRL : IAdminRL
    {
        private static string connectionString = "Data Source=DESKTOP-OKP25QH;Initial Catalog=BookStore;Integrated Security=SSPI";
        private IConfiguration Configuration { get; }
        public AdminRL(IConfiguration configuration)
        {

            Configuration = configuration;


        }

        SqlConnection connection = new SqlConnection(connectionString);


        public Admin RegisterAdminDetails(RegisterAdmin registration)
        {
            try
            {

                Admin admin = new Admin();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        connection.Open();
                        //define the SqlCommand Object
                        SqlCommand cmd = new SqlCommand("RegisterAdminDetails", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@AdminName", registration.AdminName);
                        cmd.Parameters.AddWithValue("@PhoneNumber", registration.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Email", registration.Email);
                        cmd.Parameters.AddWithValue("@Password", Password.ConvertToEncrypt(registration.Password));

                        SqlDataReader sqlDataReader = cmd.ExecuteReader();


                        if (sqlDataReader.HasRows)
                        {
                            if (sqlDataReader.Read())
                            {
                                admin.AdminID = sqlDataReader.GetInt64(0);
                                admin.AdminName = sqlDataReader.GetString(1);
                                admin.PhoneNumber = sqlDataReader.GetInt64(2);
                                admin.Email = sqlDataReader.GetString(3);

                            }
                        }

                        return admin;

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
        public string AdminLogin(LoginAdmin login)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("spInsertAdminLoginDetail", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", login.Email);
                cmd.Parameters.AddWithValue("@Password", Password.ConvertToEncrypt(login.Password));
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                Admin admin = new Admin();

                if (sqlDataReader.HasRows)
                {
                    if (sqlDataReader.Read())
                    {
                        admin.AdminID = sqlDataReader.GetInt64(0);
                        admin.AdminName = sqlDataReader.GetString(1);
                        admin.PhoneNumber = sqlDataReader.GetInt64(2);
                        admin.Email = sqlDataReader.GetString(3);

                    }
                }

                var result = returnParameter.Value;
                if (result != null && result.Equals(2))
                {
                    throw new Exception("AdminID is invalid");
                }
                if (result != null && result.Equals(3))
                {
                    throw new Exception("wrong password");
                }

                string token1 = CreateAdminToken(admin);
                return token1;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public string CreateAdminToken(Admin info)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, "Admin"),

                            new Claim("Email", info.Email.ToString() ),

                            new Claim("AdminID", info.AdminID.ToString()),

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


    }

}

