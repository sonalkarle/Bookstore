using BusinessLayer.Interface;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
   public  class CustomerOrderBL : ICustomerOrderBL
    {
        ICustomerOrderRL CustomerOrderRL;
        MSMQ mSMQ;

        public CustomerOrderBL(ICustomerOrderRL customerOrderRL, IConfiguration config)
        {
            CustomerOrderRL = customerOrderRL;
            mSMQ = new MSMQ(config);
        }
        public CustomerOrder PlaceOrder(long CustomerID, long AddressID)
        {
            try
            {
                var result = CustomerOrderRL.PlaceOrder(CustomerID, AddressID);
                mSMQ.SendOrderEmail(result);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
