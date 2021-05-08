using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface ICustomerOrderRL
    {
        public CustomerOrder PlaceOrder(long CustomerID, long AddressID);
    }
}
