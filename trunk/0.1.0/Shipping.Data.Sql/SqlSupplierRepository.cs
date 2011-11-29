using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Logging;
using System.Data.SqlClient;

using Shipping.Business.Entities;
using Shipping.Business.Entities.Collections;

namespace Shipping.Data.Sql
{
    public class SqlSupplierRepository : ISupplierRepository
    {
        private string _mainConnectionString;
        private Dictionary<string, string> _satelliteConnectionStrings;
        private ILogger _logger;

        public SqlSupplierRepository(string mainConnectionString, Dictionary<string, string> satelliteConnectionStrings, ILogger logger)
        {
            _mainConnectionString = mainConnectionString;
            _satelliteConnectionStrings = satelliteConnectionStrings;
            _logger = logger;
        }

        public Supplier GetSupplier(Guid id)
        {
            _logger.DebugFormat(this, "Get Supplier By Id {0}", id);


            return SqlUtility.ExecuteXmlStoredProcedure<Supplier>(_mainConnectionString, _logger, "Supplier_GetSupplierById", new[]
                        {
                            new SqlParameter("@Id", id)                 

                        });
        }

        public SupplierCollection GetAllSuppliers()
        {
            _logger.Debug(this, "Getting list of all Supplier");


            return SqlUtility.ExecuteXmlStoredProcedure<SupplierCollection>(_mainConnectionString, _logger, "Supplier_GetAllSuppliers");
        }

        public void AddSupplier(Business.Entities.Supplier supplier)
        {
            _logger.DebugFormat(this, "Adding Supplier for Suppiler Id {0}", supplier.Id);

            SqlUtility.ExecuteStoredProcedure(_mainConnectionString, _logger, "Supplier_AddSupplier", new[]
                        {
                            new SqlParameter("@Id", supplier.Id), 
                            new SqlParameter("@CategoryId", supplier.CategoryId), 
                            new SqlParameter("@SupplierName", supplier.SupplierName), 
                            new SqlParameter("@Address", supplier.Address), 
                            new SqlParameter("@Phone",supplier.Phone), 
                            new SqlParameter("@Fax", supplier.Fax),
                            new SqlParameter("@Email", supplier.Email)

                        });

        }

        public void UpdateSupplier(Supplier supplier)
        {
            _logger.DebugFormat(this, "Updating Supplier for Supplier Id {0}", supplier.Id);


            SqlUtility.ExecuteStoredProcedure(_mainConnectionString, _logger, "Supplier_UpdateSupplier", new[]
                        {
                           new SqlParameter("@Id", supplier.Id), 
                            new SqlParameter("@CategoryId", supplier.CategoryId), 
                            new SqlParameter("@SupplierName", supplier.SupplierName), 
                            new SqlParameter("@Address", supplier.Address), 
                            new SqlParameter("@Phone",supplier.Phone), 
                            new SqlParameter("@Fax", supplier.Fax),
                            new SqlParameter("@Email", supplier.Email)
                  });
        }

        public void DeleteSupplier(Guid Id)
        {
            _logger.DebugFormat(this, "Deleting Supplier By Id {0}", Id);


            SqlUtility.ExecuteStoredProcedure(_mainConnectionString, _logger, "Supplier_DeleteSupplierById", new[]
            {
                new SqlParameter("@Id", Id)                 

            });

        }
    }
}
