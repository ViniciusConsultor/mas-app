﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Business.Entities;

namespace Shipping.Data
{
    public interface ICustomerRepository
    {
        bool CreateCustomer(Customer customer);
        IEnumerable<Customer> GetListCustomer();
    }
}