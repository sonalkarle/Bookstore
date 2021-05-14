using BusinessLayer.IbookBL;
using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.IbookRL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.BookBL
{
    public class AdminBL: IAdminBL
    {
        IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)                      //Constructor n passing an object to IEmployeeBL
        {                                                              //to get an access of IEmployeeRL
            this.adminRL = adminRL;
        }
        public Admin RegisterAdminDetails(RegisterAdmin registration)
        {
            try
            {

                return this.adminRL.RegisterAdminDetails(registration);                 //throw exceptions
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

                return this.adminRL.AdminLogin(login);                 //throw exceptions
            }

            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
