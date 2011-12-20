﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Data;
using Shipping.Business.Entities;

namespace Shipping.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) {
            _customerRepository = customerRepository;
        }

        public bool CreateCustomer(Customer customer)
        {
            return _customerRepository.CreateCustomer(customer);
        }


        public IEnumerable<Customer> GetListCustomer()
        {
            return _customerRepository.GetListCustomer();
        }


        public Customer GetCustomerByID(Guid ID)
        {
            return _customerRepository.GetCustomerByID(ID);
        }


        public bool EditCustomer(Customer customer)
        {
            return _customerRepository.EditCustomer(customer);
        }


        public bool DeleteCustomer(Guid ID)
        {
            return _customerRepository.DeleteCustomer(ID);
        }
    }
}