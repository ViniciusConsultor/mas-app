using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public interface ISupplierService
    {
        void SaveSupplier(Supplier supplier);
        IEnumerable<Supplier> GetAllSuppliers();
        Supplier GetSupplier(Guid Id);
        void DeleteSupplier(Guid Id);
        IEnumerable<string> GetCategorySupplier();
        IEnumerable<Supplier> GetCategoryBySearch(string searchWord, string categorySearch);
    }
}
