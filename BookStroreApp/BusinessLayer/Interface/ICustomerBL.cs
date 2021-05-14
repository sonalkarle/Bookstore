using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public  interface ICustomerBL
    {
        public CustomerUser RegisterDetails(RegisterCustomer registration);
        public CustomerUser LoginCustomer(LoginCustomer loginCustomerAccount);
        public MSMQModel ForgetPassword(ForgetPasswordModel forgetPasswordModel);
        public bool ResetCustomerAccountPassword(ResetPasswordModel resetPasswordModel);
        public string GenerateToken(CustomerUser login);

    }
}
