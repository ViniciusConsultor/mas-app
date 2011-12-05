using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Data;

namespace VisitaJayaPerkasa.Business.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public void SaveSupplier(Supplier supplier)
        {
            _supplierRepository.SaveSupplier(supplier);
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return _supplierRepository.GetAllSuppliers();
        }

        public Supplier GetSupplier(Guid id)
        {
            return _supplierRepository.GetSupplier(id);
        }

        public void DeleteSupplier(Guid Id)
        {
            _supplierRepository.DeleteSupplier(Id);
        }
    }
}
