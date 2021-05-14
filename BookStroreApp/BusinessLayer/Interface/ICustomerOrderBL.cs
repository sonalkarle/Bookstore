using CommonLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public  interface ICustomerOrderBL
    {

        public CustomerOrder PlaceOrder(long CustomerID, long AddressID);

    }
}
