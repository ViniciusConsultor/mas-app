using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Business.Entities;
using Shipping.Data;
using Shipping.Business.Entities.Collections;

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

        public SupplierCollection GetAllSuppliers()
        {
            return _supplierRepository.GetAllSuppliers();
        }

        public Supplier GetSupplier(Guid id)
        {
            return _supplierRepository.GetSupplier(id);
        }
    }
}
