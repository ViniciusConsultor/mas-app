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
                        "SELECT DISTINCT c.* FROM [Category] c, [SUPPLIER] s WHERE (c.deleted = '0' OR c.deleted is null) AND " +
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
                            supplier.Id = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
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


        public List<PriceList> GetPriceListByCriteria(DateTime from, DateTime to, string supplierID, string destinationID, 
            string customerID, string recipientID, string stuffingID, byte isSupplier, string typeContID) {
            List<PriceList> listPriceList = null;
            
            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    string addCriteria;
                    if(isSupplier == 1)
                        addCriteria = "(price_customer is null OR price_customer = 0) ";
                    else
                        addCriteria = "(price_supplier is null OR price_supplier = 0) AND type_cont_id like '" + typeContID + "' ";

                    //issupplier == 1 so, is search from supplier
                    using (SqlCommand command = new SqlCommand(
                        "SELECT * FROM [Price] WHERE (date between '" + from + "' AND '" + to + "') " + 
                        "AND supplier_id like '%" + supplierID + "%' AND destination like '%" + destinationID + "%' " + 
                        "AND customer_id like '%" + customerID + "%' AND recipient_id like '%" + recipientID + "%' " + 
                        "AND stuffing_id like '%" + stuffingID + "%' AND " +
                        addCriteria + 
                        "ORDER BY date"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            PriceList objPriceList = new PriceList();
                            objPriceList.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            objPriceList.Date = reader.GetDateTime(1);
                            objPriceList.SupplierID = Utility.Utility.ConvertToUUID(reader.GetValue(2).ToString());
                            objPriceList.Destination = Utility.Utility.ConvertToUUID(reader.GetValue(3).ToString());
                            objPriceList.TypeID = Utility.Utility.ConvertToUUID(reader.GetValue(4).ToString());
                            objPriceList.ConditionID = Utility.Utility.ConvertToUUID(reader.GetValue(5).ToString());
                            objPriceList.PriceSupplier = reader.GetDecimal(6);
                            objPriceList.CustomerID = Utility.Utility.ConvertToUUID(reader.GetValue(7).ToString());
                            objPriceList.PriceCustomer = reader.GetDecimal(8);
                            objPriceList.StuffingID = Utility.Utility.ConvertToUUID(reader.GetValue(9).ToString());
                            objPriceList.Recipient = Utility.Utility.ConvertToUUID(reader.GetValue(10).ToString());
                            objPriceList.PriceCourier = reader.GetDecimal(11);

                            if (listPriceList == null)
                                listPriceList = new List<PriceList>();

                            listPriceList.Add(objPriceList);
                            objPriceList = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPriceListRepository.cs - GetPriceListByDate() " + e.Message);
            }

            return listPriceList;
        }
        

        public bool SavePriceList(SqlParameter[] sqlParamDeleted, SqlParameter[] sqlParamInsert) {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();

                    if (sqlParamDeleted != null)
                    {
                        for (int i = 0; i < sqlParamDeleted.Length; i++)
                        {
                            using (SqlCommand command = new SqlCommand(
                                "delete [Price] WHERE price_id = " + sqlParamDeleted[i].ParameterName, con))
                            {
                                command.Transaction = sqlTransaction;
                                command.Parameters.Add(sqlParamDeleted[i]);
                                n = command.ExecuteNonQuery();
                            }

                            if (n == 0)
                                break;
                        }
                    } 
//                    if (n > 0  ||  sqlParamDeleted == null)
//                    { 
                        n = 0;

                        for (int i = 0; i < sqlParamInsert.Length; )
                        {
                            using (SqlCommand command = new SqlCommand(
                                    "Insert Into [Price] Values (" +
                                    sqlParamInsert[i++].ParameterName + ", " +
                                    sqlParamInsert[i++].ParameterName + ", " +
                                    sqlParamInsert[i++].ParameterName + ", " +
                                    sqlParamInsert[i++].ParameterName + ", " +
                                    sqlParamInsert[i++].ParameterName + ", " +
                                    sqlParamInsert[i++].ParameterName + ", " +
                                    sqlParamInsert[i++].ParameterName + ", " +
                                    sqlParamInsert[i++].ParameterName + ", " +
                                    sqlParamInsert[i++].ParameterName + ", " +
                                    sqlParamInsert[i++].ParameterName + ", " + 
                                    sqlParamInsert[i++].ParameterName + ", " +
                                    sqlParamInsert[i++].ParameterName +  
                                    ")"
                                    , con))
                            {
                                command.Transaction = sqlTransaction;

                                //11 is field of price list
                                for (int k = i - 12; k < i; k++)
                                    command.Parameters.Add(sqlParamInsert[k]);
                                n = command.ExecuteNonQuery();

                                if (n == 0)
                                    break;
                            }
                        }

                        if (n > 0)
                            sqlTransaction.Commit();
                        else
                            sqlTransaction.Commit();
//                    }
//                    else
//                        sqlTransaction.Rollback();
                }
                catch (Exception e)
                {
                    if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    Logging.Error("SqlPriceListRepository.cs - SavePriceList() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


        public decimal SearchPriceList(DateTime date, string customerID, string destinationID,
            string typeContID, string conditionID)
        {
            decimal result = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT price_customer FROM [Price] WHERE date <= '" + date + "' " +
                        "AND customer_id like '%" + customerID + "%' AND destination like '%" + destinationID + "%' " +
                        "AND type_cont_id like '%" + typeContID + "%' AND condition_id like '%" + conditionID + "%' " +
                        "ORDER BY date DESC "
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            result = reader.GetDecimal(0);
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPriceListRepository.cs - SearchPriceList() " + e.Message);
            }

            return result;
        }

        public int FindPriceByDateSupplierCustomer(DateTime date, string supplierID, string customerID)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT COUNT(*) FROM [Price] " +
                        "WHERE [date] = '" + date + "' " +
                        "AND supplier_id like '%" + supplierID + "%' " +
                        "AND customer_id like '%" + customerID + "%'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPriceListRepository.cs - FindPriceByDateSupplierCustomer() " + e.Message);
            }

            return result;
        }

        public VisitaJayaPerkasa.Entities.PriceList GetPriceByDateSupplierCustomer(DateTime date, string supplierID, string customerID)
        {
            VisitaJayaPerkasa.Entities.PriceList result = new VisitaJayaPerkasa.Entities.PriceList();
            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT * FROM [Price] " +
                        "WHERE [date] = '" + date + "' " +
                        "AND supplier_id like '%" + supplierID + "%' " +
                        "AND customer_id like '%" + customerID + "%'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            result.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            result.Date = reader.GetDateTime(1);
                            result.SupplierID = Utility.Utility.ConvertToUUID(reader.GetValue(2).ToString());
                            result.Destination = Utility.Utility.ConvertToUUID(reader.GetValue(3).ToString());
                            result.TypeID = Utility.Utility.ConvertToUUID(reader.GetValue(4).ToString());
                            result.ConditionID = Utility.Utility.ConvertToUUID(reader.GetValue(5).ToString());
                            result.PriceSupplier = reader.GetDecimal(6);
                            result.CustomerID = Utility.Utility.ConvertToUUID(reader.GetValue(7).ToString());
                            result.PriceCustomer = reader.GetDecimal(8);
                            result.StuffingID = Utility.Utility.ConvertToUUID(reader.GetValue(9).ToString());
                            result.Recipient = Utility.Utility.ConvertToUUID(reader.GetValue(10).ToString());
                            result.PriceCourier = reader.GetDecimal(11);
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPriceListRepository.cs - GetPriceByDateSupplierCustomer() " + e.Message);
            }

            return result;
        }

        public string FindPriceBySupplierCustomerStuffing(DateTime date, string supplierID, string customerID, string stuffingID, string typeContID)
        {
            string result = "";
            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT 'Supplier: ' + ISNULL(s.supplier_name, 'null') + CHAR(13) + CHAR(10) + 'Customer: ' + ISNULL(c.customer_name, 'null') + CHAR(13) + CHAR(10) + 'Stuffing place: ' + ISNULL(w.address, 'null') + CHAR(13) + CHAR(10) + 'Type Cont: ' + ISNULL(t.type_name, 'null') + CHAR(13) + CHAR(10) + 'on ' + convert(varchar, p.[date], 106) " +
                        "FROM [ShippingMain].[dbo].[PRICE] p " +
                        "LEFT OUTER JOIN [ShippingMain].[dbo].[SUPPLIER] s " +
                        "ON s.supplier_id = p.supplier_id " +
                        "LEFT OUTER JOIN [ShippingMain].[dbo].[CUSTOMER] c " +
                        "ON c.customer_id = p.customer_id " +
                        "LEFT OUTER JOIN [ShippingMain].[dbo].[TYPE_CONT] t " +
                        "ON t.type_id = p.type_cont_id " +
                        "LEFT OUTER JOIN [ShippingMain].[dbo].[WAREHOUSE] w " +
                        "ON w.stuffing_place_id = p.stuffing_id " +
                        "WHERE p.[date] = '" + date + "' " +
                        "AND p.supplier_id = '" + supplierID + "' " +
                        "AND p.customer_id = '" + customerID + "' " +
                        "AND p.stuffing_id = '" + stuffingID + "' " +
                        "AND p.type_cont_id = '" + typeContID + "'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            result = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPriceListRepository.cs - FindPriceBySupplierCustomerStuffing() " + e.Message);
            }

            return result;
        }

        public VisitaJayaPerkasa.Entities.PriceList GetPriceBySupplierCustomerStuffing(DateTime date, string supplierID, string customerID, string stuffingID, string typeContID)
        {
            VisitaJayaPerkasa.Entities.PriceList result = new VisitaJayaPerkasa.Entities.PriceList();
            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT * FROM [Price] " +
                        "WHERE [date] = '" + date + "' " +
                        "AND supplier_id = '" + supplierID + "' " +
                        "AND customer_id = '" + customerID + "' " +
                        "AND stuffing_id = '" + stuffingID + "' " +
                        "AND type_cont_id = '" + typeContID + "'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            result.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            result.Date = reader.GetDateTime(1);
                            result.SupplierID = Utility.Utility.ConvertToUUID(reader.GetValue(2).ToString());
                            result.Destination = Utility.Utility.ConvertToUUID(reader.GetValue(3).ToString());
                            result.TypeID = Utility.Utility.ConvertToUUID(reader.GetValue(4).ToString());
                            result.ConditionID = Utility.Utility.ConvertToUUID(reader.GetValue(5).ToString());
                            result.PriceSupplier = reader.GetDecimal(6);
                            result.CustomerID = Utility.Utility.ConvertToUUID(reader.GetValue(7).ToString());
                            result.PriceCustomer = reader.GetDecimal(8);
                            result.StuffingID = Utility.Utility.ConvertToUUID(reader.GetValue(9).ToString());
                            result.Recipient = Utility.Utility.ConvertToUUID(reader.GetValue(10).ToString());
                            result.PriceCourier = reader.GetDecimal(11);
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPriceListRepository.cs - GetPriceBySupplierCustomerStuffing() " + e.Message);
            }

            return result;
        }
    }
}
