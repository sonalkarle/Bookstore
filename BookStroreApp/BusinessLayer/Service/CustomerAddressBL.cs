using BusinessLayer.Interface;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
   public  class CustomerAddressBL : ICustomerAddressBL
    {
        ICustomerAddressRL customerAddressRL;

        public CustomerAddressBL(ICustomerAddressRL customerAddressRL)
        {
            this.customerAddressRL = customerAddressRL;
        }

        public CustomAddressResponse AddCustomerAddress(CustomerAddress address)
        {
            try
            {
                return customerAddressRL.AddCustomerAddress(address); ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCustomerAddress(long customerID, long addressID)
        {
            try
            {
                return customerAddressRL.DeleteCustomerAddress(customerID, addressID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<CustomAddressResponse> GetAllCustomerAddress(long customerID)
        {
            try
            {
                return customerAddressRL.GetAllCustomerAddress(customerID); ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CustomAddressResponse UpdateCustomerAddress(CustomerAddress address)
        {
            try
            {
                return customerAddressRL.UpdateCustomerAddress(address); ;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
