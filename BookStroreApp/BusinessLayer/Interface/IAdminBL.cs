using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.IbookBL
{
    public interface IAdminBL
    {
        public Admin RegisterAdminDetails(RegisterAdmin registration);
        public string AdminLogin(LoginAdmin login);

    }
}
