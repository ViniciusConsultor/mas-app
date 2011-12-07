using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Data;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) {
            _customerRepository = customerRepository;
        }

        public void SaveCustomer(Customer customer)
        {
            _customerRepository.SaveCustomer(customer);
        }


        public IEnumerable<Customer> GetListCustomer()
        {
            return _customerRepository.GetListCustomer();
        }


        public Customer GetCustomerByID(Guid ID)
        {
            return _customerRepository.GetCustomerByID(ID);
        }

        public void DeleteCustomer(Guid ID)
        {
            _customerRepository.DeleteCustomer(ID);
        }


        public IEnumerable<Customer> GetCustomerBySearch(string searchWord)
        {
            return _customerRepository.GetCustomerBySearch(searchWord);
        }
    }
}
