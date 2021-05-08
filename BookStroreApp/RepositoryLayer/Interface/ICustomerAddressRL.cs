using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public  interface ICustomerAddressRL
    {
        CustomAddressResponse AddCustomerAddress(CustomerAddress address);
        bool DeleteCustomerAddress(long customerID, long addressID);
        ICollection<CustomAddressResponse> GetAllCustomerAddress(long customerID);
        CustomAddressResponse UpdateCustomerAddress(CustomerAddress address);
    }
}
