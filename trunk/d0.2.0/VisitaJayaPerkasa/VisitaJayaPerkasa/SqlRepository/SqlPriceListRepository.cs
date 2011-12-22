﻿using System;
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


        public List<PriceList> GetPriceListByCriteria(DateTime from, DateTime to, string supplierID, string destinationID) {
            List<PriceList> listPriceList = null;
            
            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT * FROM [Price] WHERE (date between '" + from + "' AND '" + to + "') AND supplier_id like '" + supplierID + "' AND destination like '" + destinationID + "'" 
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
                            objPriceList.Price = reader.GetDecimal(6);

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

            try
            {
                using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
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

                    if (n > 0  ||  sqlParamDeleted == null)
                    {
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
                                    sqlParamInsert[i++].ParameterName + 
                                    ")"
                                    , con))
                            {
                                command.Transaction = sqlTransaction;

                                //7 is field of price list
                                for (int k = i - 7; k < i; k++)
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
                    }
                    else
                        sqlTransaction.Rollback();
                }
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

            return n > 0;
        }
    }
}