using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.IbookRL
{
   public  interface IAdminRL
    {
        public Admin RegisterAdminDetails(RegisterAdmin registration);
        public string AdminLogin(LoginAdmin login);

    }
}
