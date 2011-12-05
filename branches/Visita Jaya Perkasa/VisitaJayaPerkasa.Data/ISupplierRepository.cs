using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data
{
    public interface ISupplierRepository
    {
        void SaveSupplier(Supplier supplier);
        IEnumerable<Supplier> GetAllSuppliers();
        Supplier GetSupplier(Guid Id);
        void DeleteSupplier(Guid Id);
    }
}
