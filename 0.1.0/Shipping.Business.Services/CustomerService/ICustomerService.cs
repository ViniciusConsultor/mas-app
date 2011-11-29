using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Business.Entities;

namespace Shipping.Business.Services
{
    public interface ICustomerService
    {
        bool CreateCustomer(Customer customer);
        IEnumerable<Customer> GetListCustomer();
    }
}
