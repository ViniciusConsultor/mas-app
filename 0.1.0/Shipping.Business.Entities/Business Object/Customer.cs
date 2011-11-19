﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Business.Entities
{
    public class Customer
    {
        public virtual Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string Office { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
    }
}
