using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data.Sql
{
    public class SupplierRepository : ISupplierRepository
    {
        private string _mainConnectionString;

        public SupplierRepository(string mainConnectionString)
        {
            _mainConnectionString = mainConnectionString;
        }

        public Supplier GetSupplier(Guid id)
        {

            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            Supplier supplier = repo.SingleOrDefault<Supplier>("SELECT * FROM SUPPLIER WHERE supplier_id=@0", id);

            repo.CloseSharedConnection();

            return supplier;
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            List<Supplier> suppliers = repo.Fetch<Supplier>("SELECT * FROM [SUPPLIER]").ToList<Supplier>();

            repo.CloseSharedConnection();

            return suppliers;
        }

        public void SaveSupplier(Supplier supplier)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            using (var scope = repo.GetTransaction())
            {
                if (GetSupplier(supplier.Id) == null)
                {
                    //Create new
                    repo.Insert(supplier);
                }
                else
                {
                    //Update it

                    repo.Update("SUPPLIER", "supplier_id", supplier);
                }

                scope.Complete();
            }

            repo.CloseSharedConnection();
        }

        public void DeleteSupplier(Guid Id)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();
            Supplier supplier = GetSupplier(Id);
            repo.Delete("SUPPLIER", "supplier_id", supplier);
            repo.CloseSharedConnection();
        }


        public IEnumerable<string> GetCategorySupplier()
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();

            IEnumerable<string> supplierCategory = repo.Fetch<string>(
                "SELECT DISTINCT category_code FROM SUPPLIER WHERE deleted is null OR deleted = '0'"
                ).ToList();

            repo.CloseSharedConnection();
            return supplierCategory;
        }


        public IEnumerable<Supplier> GetCategoryBySearch(string searchWord, string categorySearch)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();

            IEnumerable<Supplier> listSuppliers = repo.Fetch<Supplier>(
                "SELECT * FROM SUPPLIER WHERE (deleted is null or deleted = '0') AND " + 
                "(category_code like '%" + categorySearch + "%' OR supplier_name like '%" + categorySearch + "%')" +
                " AND (address like '%" + searchWord + "%' OR phone like '%" + searchWord + "%'" + 
                " OR fax like '%" + searchWord + "%' OR email like '%" + searchWord + "%' OR contact_person like '%" + searchWord + "%')"
                ).ToList();

            repo.CloseSharedConnection();
            return listSuppliers;
        }
    }
}
