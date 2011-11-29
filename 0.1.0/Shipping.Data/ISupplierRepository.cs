﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Business.Entities;
using Shipping.Business.Entities.Collections;

namespace Shipping.Data
{
    public interface ISupplierRepository
    {
        void AddSupplier(Supplier supplier);
        SupplierCollection GetAllSuppliers();
        Supplier GetSupplier(Guid Id);
        void UpdateSupplier(Supplier supplier);
        void DeleteSupplier(Guid Id);
    }
}
