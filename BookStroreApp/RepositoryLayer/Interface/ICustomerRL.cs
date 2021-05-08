using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public  interface ICustomerRL
    {
        public CustomerUser RegisterDetails(RegisterCustomer registration);
        public CustomerUser LoginCustome(LoginCustomer loginCustomerAccount);
        public MSMQModel ForgetPassword(ForgetPasswordModel forgetPasswordModel);
        bool ResetCustomerAccountPassword(ResetPasswordModel resetPasswordModel);
        public string CreateToken(CustomerUser info);

    }
}
