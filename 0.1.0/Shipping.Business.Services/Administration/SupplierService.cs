using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Business.Entities;
using Shipping.Data;

namespace Shipping.Business.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public void AddSupplier(Supplier supplier)
        {
            _supplierRepository.AddSupplier(supplier);
        }
    }
}
