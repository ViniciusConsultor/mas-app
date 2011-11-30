using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Business.Entities;

namespace Shipping.Data
{
    public interface ICustomerRepository
    {
        bool CreateCustomer(Customer customer);
        bool EditCustomer(Customer customer);
        bool DeleteCustomer(Guid ID);
        IEnumerable<Customer> GetListCustomer();
        Customer GetCustomerByID(Guid ID);
    }
}
