using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CustomerBL: ICustomerBL
    {
        ICustomerRL userRL;
        public CustomerBL(ICustomerRL userRL)                      //Constructor n passing an object to IEmployeeBL
        {                                                              //to get an access of IEmployeeRL
            this.userRL = userRL;
        }

        public CustomerUser RegisterDetails(RegisterCustomer registration)
        {
            try
            {

                return this.userRL.RegisterDetails(registration);                 //throw exceptions
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public CustomerUser LoginCustomer(LoginCustomer loginCustomerAccount)
        {
            try
            {
                loginCustomerAccount.Password = Password.ConvertToEncrypt(loginCustomerAccount.Password);

                var result = userRL.LoginCustome(loginCustomerAccount);
                if (result != null)
                {
                    result.token = userRL.CreateToken(result);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MSMQModel ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {

                return this.userRL.ForgetPassword(forgetPasswordModel);                 //throw exceptions
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
              
                return userRL.ResetCustomerAccountPassword(resetPasswordModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateToken(CustomerUser login)
        {
            try
            {

                return this.userRL.CreateToken(login);                 //throw exceptions
            }

            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
