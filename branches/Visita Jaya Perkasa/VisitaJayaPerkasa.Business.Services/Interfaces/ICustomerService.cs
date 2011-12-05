using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public interface ICustomerService
    {
        void SaveCustomer(Customer customer);
        void DeleteCustomer(Guid ID);
        IEnumerable<Customer> GetListCustomer();
        Customer GetCustomerByID(Guid ID);
    }
}
