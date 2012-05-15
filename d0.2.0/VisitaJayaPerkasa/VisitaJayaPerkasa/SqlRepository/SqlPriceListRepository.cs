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

        //isAtkTypeOfSupplier : 0 is false     AND      1 is true
        public List<Category> GetTypeOfSupplier(byte isAtkTypeOfSupplier)
        {
            List<Category> listCategory = null;
            string criteria;

            if (isAtkTypeOfSupplier == 0)
                criteria = " AND category_name not like '%General%' ";
            else
                criteria = " AND category_name like '%General%' ";

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT DISTINCT c.* FROM [Category] c, [SUPPLIER] s WHERE (c.deleted = '0' OR c.deleted is null) AND " +
                        "(s.deleted = '0' OR s.deleted is null) AND c.category_id = s.category_id" + criteria + 
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

        public bool DeletePrice(Guid ID) {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    sqlTransaction = con.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(
                        "delete [Price] WHERE price_id = '" + ID + "'", con))
                    {
                        command.Transaction = sqlTransaction;
                        n = command.ExecuteNonQuery();
                    }

                    if (n > 0)
                        sqlTransaction.Commit();
                }
                catch (Exception e)
                {
                    if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    Logging.Error("SqlPriceListRepository.cs - DeletePrice() " + e.Message);
                    n = 0;
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public List<Supplier> GetSupplier(Guid ID)
        {
            List<Supplier> listSupplier = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

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
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    string addCriteria;
                    if(isSupplier == 1)
                        addCriteria = "(price_customer is null OR price_customer = 0) ";
                    else
                        addCriteria = "(price_supplier is null OR price_supplier = 0) AND type_cont_id like '" + typeContID + "' ";

                    //issupplier == 1 so, is search from supplier
                    using (SqlCommand command = new SqlCommand(
                        "SELECT p.*, (SELECT TOP 1 supplier_name FROM supplier s WHERE s.supplier_id = p.supplier_id) as supplierName FROM [Price] p WHERE " +
                        "((cast(dateFrom as date) <= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date) " +
	                    "AND cast(dateto as date) >= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date)) " +
                        "OR " +
                        "(cast(dateFrom as date) >= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date) " +
                        "AND cast(dateFrom as date) <= cast('" + Utility.Utility.ConvertDateToString(to) + "' as date))) " + 
                        "AND supplier_id like '%" + supplierID + "%' AND destination like '%" + destinationID + "%' " + 
                        "AND customer_id like '%" + customerID + "%' AND recipient_id like '%" + recipientID + "%' " + 
                        "AND stuffing_id like '%" + stuffingID + "%' AND " +
                        addCriteria + 
                        "ORDER BY dateFrom"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            PriceList objPriceList = new PriceList();
                            objPriceList.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            objPriceList.DateFrom = reader.GetDateTime(1);
                            objPriceList.DateTo = Utility.Utility.IsDBNull(reader.GetValue(2)) ? Utility.Utility.DefaultDateTime() : reader.GetDateTime(2);
                            objPriceList.SupplierID = Utility.Utility.ConvertToUUID(reader.GetValue(3).ToString());
                            objPriceList.Destination = Utility.Utility.ConvertToUUID(reader.GetValue(4).ToString());
                            objPriceList.TypeID = Utility.Utility.ConvertToUUID(reader.GetValue(5).ToString());
                            objPriceList.ConditionID = Utility.Utility.ConvertToUUID(reader.GetValue(6).ToString());
                            objPriceList.PriceSupplier = Utility.Utility.IsDBNull(reader.GetValue(7)) ? 0 : reader.GetDecimal(7);
                            objPriceList.CustomerID = Utility.Utility.ConvertToUUID(reader.GetValue(8).ToString());
                            objPriceList.PriceCustomer = Utility.Utility.IsDBNull(reader.GetValue(9)) ? 0 : reader.GetDecimal(9);
                            objPriceList.StuffingID = Utility.Utility.ConvertToUUID(reader.GetValue(10).ToString());
                            objPriceList.Recipient = Utility.Utility.ConvertToUUID(reader.GetValue(11).ToString());
                            objPriceList.PriceCourier = Utility.Utility.IsDBNull(reader.GetValue(12)) ? 0 : reader.GetDecimal(12);
                            objPriceList.Item = Utility.Utility.IsDBNull(reader.GetValue(13)) ? null : reader.GetString(13);
                            objPriceList.SupplierName = Utility.Utility.IsDBNull(reader.GetValue(14)) ? null : reader.GetString(14);

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
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

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

                    if (n > 0  ||  sqlParamDeleted == null)
                    { 
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
                                    sqlParamInsert[i++].ParameterName + ", " +
                                    sqlParamInsert[i++].ParameterName + ", " +
                                    sqlParamInsert[i++].ParameterName +  
                                    ")"
                                    , con))
                            {
                                command.Transaction = sqlTransaction;

                                //14 is field of price list
                                for (int k = i - 14; k < i; k++)
                                    command.Parameters.Add(sqlParamInsert[k]);
                                n = command.ExecuteNonQuery();

                                if (n == 0)
                                    break;
                            }
                        }

                        if (n > 0)
                            sqlTransaction.Commit();
                    }
                    else
                        sqlTransaction.Rollback();
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
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    string query =
                        "SELECT TOP 1 p.price_customer, c.status_ppn FROM [Price] p " +
                        "INNER JOIN [customer] c ON c.customer_id = p.customer_id " +
                        "WHERE (cast(p.dateFrom as date) <= cast('" + Utility.Utility.ConvertDateToString(date) + "' as date) " +
                        "AND cast(p.dateto as date) >= cast('" + Utility.Utility.ConvertDateToString(date) + "' as date)) " +
                        "AND p.customer_id like '%" + customerID + "%' AND p.destination like '%" + destinationID + "%' " +
                        "AND p.type_cont_id like '%" + typeContID + "%' AND p.condition_id like '%" + conditionID + "%' " +
                        "AND p.condition_id like '%" + conditionID + "%' " +
                        "ORDER BY dateFrom DESC ";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            result = reader.GetDecimal(0);
                            bool ppn = reader.GetBoolean(1);
                            if (ppn)
                                result = Math.Ceiling(result * (decimal)1.1);
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


        public decimal SearchPriceListGeneral(DateTime date, string destinationID,
            string typeContID, string conditionID)
        {
            decimal result = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    string query =
                        "SELECT TOP 1 p.price_customer, c.status_ppn FROM [Price] p " +
                        "INNER JOIN [customer] c ON c.customer_id = p.customer_id " +
                        "WHERE (cast(p.dateFrom as date) <= cast('" + Utility.Utility.ConvertDateToString(date) + "' as date) " +
                        "AND cast(p.dateto as date) >= cast('" + Utility.Utility.ConvertDateToString(date) + "' as date)) " +
                        "AND p.destination like '%" + destinationID + "%' " +
                        "AND p.type_cont_id like '%" + typeContID + "%' AND p.condition_id like '%" + conditionID + "%' " +
                        "ORDER BY dateFrom DESC ";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            result = reader.GetDecimal(0);
                            bool ppn = reader.GetBoolean(1);
                            if (ppn)
                                result = Math.Ceiling(result * (decimal)1.1);
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



        /* Search Price list for Check exist or not by criteria*/
        public Guid GetPriceCustomerByShippingLines(DateTime from, DateTime to, string typeContID, string conditionID, 
            string supplierID, string destinationID, string customerID)
        {
            Guid ID = Guid.Empty;
           
            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 price_id FROM [Price] " +
                        "WHERE ((cast(dateFrom as date) <= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date) " +
                        "AND cast(dateto as date) >= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date)) " +
                        "OR " +
                        "(cast(dateFrom as date) >= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date) " +
                        "AND cast(dateFrom as date) <= cast('" + Utility.Utility.ConvertDateToString(to) + "' as date))) " + 
                        "AND type_cont_id = '" + typeContID + "' " + 
                        "AND condition_id = '" + conditionID + "' " + 
                        "AND customer_id = '" + customerID + "' " + 
                        "AND destination = '" + destinationID + "' " + 
                        "AND supplier_id = '" + supplierID + "' " + 
                        "AND "
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPriceListRepository.cs - GetPriceCustomerByShippingLines() " + e.Message);
            }

            return ID;
        }



        public Guid GetPriceCustomerByTrucking(DateTime from, DateTime to, string typeContID, string supplierID, 
            string customerID, string stuffingID)
        {
            Guid ID = Guid.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 price_id FROM [Price] " +
                        "WHERE ((cast(dateFrom as date) <= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date) " +
                        "AND cast(dateto as date) >= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date)) " +
                        "OR " +
                        "(cast(dateFrom as date) >= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date) " +
                        "AND cast(dateFrom as date) <= cast('" + Utility.Utility.ConvertDateToString(to) + "' as date))) " + 
                        "AND type_cont_id = '" + typeContID + "' " + 
                        "AND supplier_id = '" + supplierID + "' " + 
                        "AND customer_id = '" + customerID + "' " + 
                        "AND stuffing_id = '" + stuffingID + "'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPriceListRepository.cs - GetPriceCustomerByDAgentANDTrucking() " + e.Message);
            }

            return ID;
        }



        public Guid GetPriceCustomerByDooringAgent(DateTime from, DateTime to, string typeContID, string supplierID,
            string destinationID, string recipientID)
        {
            Guid ID = Guid.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 price_id FROM [Price] " +
                        "WHERE ((cast(dateFrom as date) <= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date) " +
                        "AND cast(dateto as date) >= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date)) " +
                        "OR " +
                        "(cast(dateFrom as date) >= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date) " +
                        "AND cast(dateFrom as date) <= cast('" + Utility.Utility.ConvertDateToString(to) + "' as date))) " +
                        "AND type_cont_id = '" + typeContID + "' " +
                        "AND supplier_id = '" + supplierID + "' " +
                        "AND destination = '" + destinationID + "' " +
                        "AND recipient_id = '" + recipientID + "'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPriceListRepository.cs - GetPriceCustomerByDAgentANDTrucking() " + e.Message);
            }

            return ID;
        }




        public bool EditPriceATK(SqlParameter[] sqlParamEdit) {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    sqlTransaction = con.BeginTransaction();

                    using (SqlCommand command = new SqlCommand(
                        "Update [Price] set supplier_id = " + sqlParamEdit[0].ParameterName + 
                        ", dateFrom = " + sqlParamEdit[1].ParameterName + 
                        ", item = " + sqlParamEdit[2].ParameterName + 
                        ", price_supplier = " + sqlParamEdit[3].ParameterName +
                        " WHERE price_id = " + sqlParamEdit[4].ParameterName, con))
                    {
                        command.Transaction = sqlTransaction;
                        for (int i = 0; i < sqlParamEdit.Length; i++)
                            command.Parameters.Add(sqlParamEdit[i]);

                        n = command.ExecuteNonQuery();
                    }

                    if (n > 0)
                        sqlTransaction.Commit();
                }
                catch (Exception e)
                {
                    if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    Logging.Error("SqlPriceListRepository.cs - EditPriceATK() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


        public bool GetExistsDatePriceATK(SqlParameter[] sqlParam, bool isCreate)
        {
            string criteria = "";

            bool exist = false;
            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    if (isCreate)
                        criteria = "";
                    else
                        criteria = " AND price_id != '" + sqlParam[3].Value + "' ";


                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 price_id FROM [Price] " +
                        "WHERE cast(dateFrom as date) = cast(" + sqlParam[0].ParameterName + " as date)" +  
                        " AND item = " + sqlParam[1].ParameterName + 
                        " AND supplier_id = " + sqlParam[2].ParameterName +
                        criteria
                        , con))
                    {
                        command.Parameters.Add(sqlParam[0]);
                        command.Parameters.Add(sqlParam[1]);
                        command.Parameters.Add(sqlParam[2]);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            exist = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPriceListRepository.cs - GetExistsDatePriceATK() " + e.Message);
            }

            return exist;
        }


        public Guid GetPriceMenuCustomer(DateTime from, DateTime to, string conditionID, string typeID, string destinationID, string customerID)
        {
            Guid ID = Guid.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 price_id FROM [Price] " +
                        "WHERE ((cast(dateFrom as date) <= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date) " +
                        "AND cast(dateto as date) >= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date)) " +
                        "OR " +
                        "(cast(dateFrom as date) >= cast('" + Utility.Utility.ConvertDateToString(from) + "' as date) " +
                        "AND cast(dateFrom as date) <= cast('" + Utility.Utility.ConvertDateToString(to) + "' as date))) " +
                        "AND condition_id = '" + conditionID + "' " + 
                        "AND type_cont_id = '" + typeID + "' " + 
                        "AND destination = '" + destinationID + "' " +
                        "AND customer_id = '" + customerID + "'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPriceListRepository.cs - GetPriceMenuCustomer() " + e.Message);
            }

            return ID;
        }


        /*Finish search */


        /*
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
                            result.DateFrom = reader.GetDateTime(1);
                            result.DateTo = reader.GetDateTime(2);
                            result.SupplierID = Utility.Utility.ConvertToUUID(reader.GetValue(3).ToString());
                            result.Destination = Utility.Utility.ConvertToUUID(reader.GetValue(4).ToString());
                            result.TypeID = Utility.Utility.ConvertToUUID(reader.GetValue(5).ToString());
                            result.ConditionID = Utility.Utility.ConvertToUUID(reader.GetValue(6).ToString());
                            result.PriceSupplier = reader.GetDecimal(7);
                            result.CustomerID = Utility.Utility.ConvertToUUID(reader.GetValue(8).ToString());
                            result.PriceCustomer = reader.GetDecimal(9);
                            result.StuffingID = Utility.Utility.ConvertToUUID(reader.GetValue(10).ToString());
                            result.Recipient = Utility.Utility.ConvertToUUID(reader.GetValue(11).ToString());
                            result.PriceCourier = reader.GetDecimal(12);
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
                            result.DateFrom = reader.GetDateTime(1);
                            result.DateTo = reader.GetDateTime(2);
                            result.SupplierID = Utility.Utility.ConvertToUUID(reader.GetValue(3).ToString());
                            result.Destination = Utility.Utility.ConvertToUUID(reader.GetValue(4).ToString());
                            result.TypeID = Utility.Utility.ConvertToUUID(reader.GetValue(5).ToString());
                            result.ConditionID = Utility.Utility.ConvertToUUID(reader.GetValue(6).ToString());
                            result.PriceSupplier = reader.GetDecimal(7);
                            result.CustomerID = Utility.Utility.ConvertToUUID(reader.GetValue(8).ToString());
                            result.PriceCustomer = reader.GetDecimal(9);
                            result.StuffingID = Utility.Utility.ConvertToUUID(reader.GetValue(10).ToString());
                            result.Recipient = Utility.Utility.ConvertToUUID(reader.GetValue(11).ToString());
                            result.PriceCourier = reader.GetDecimal(12);
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

        */
    }
}
