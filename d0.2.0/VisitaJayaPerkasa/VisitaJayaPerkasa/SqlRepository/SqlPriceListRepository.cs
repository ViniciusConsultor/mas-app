using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlPriceListRepository
    {
        public List<Category> GetTypeOfSupplier()
        {
            List<Category> listCategory = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT c.* FROM [Category] c, [SUPPLIER] s WHERE (c.deleted = '0' OR c.deleted is null) AND " +
                        "(s.deleted = '0' OR s.deleted is null) AND c.category_id = s.category_id " +
                        "ORDER BY category_name ASC"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Category category = new Category();
                            category.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            category.CategoryCode = reader.GetString(1);
                            category.CategoryName = reader.GetString(2);

                            if (listCategory == null)
                                listCategory = new List<Category>();

                            listCategory.Add(category);
                            category = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPriceListRepository.cs - GetTypeOfSupplier() " + e.Message);
            }

            return listCategory;
        }

        public List<Supplier> GetSupplier(Guid ID)
        {
            List<Supplier> listSupplier = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT supplier_id, supplier_name FROM SUPPLIER " + 
                        "WHERE (deleted is null OR deleted = '0') AND category_id like '" + ID + "' ORDER BY supplier_name ASC"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Supplier supplier = new Supplier();
                            supplier.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            supplier.SupplierName = reader.GetString(1); 

                            if (listSupplier == null)
                                listSupplier = new List<Supplier>();

                            listSupplier.Add(supplier);
                            supplier = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPriceListRepository.cs - GetSupplier() " + e.Message);
            }

            return listSupplier;
        }
    }
}
