﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlCustomerTransRepository
    {
        public List<CustomerTrans> ListCustomerTrans()
        {
            List<CustomerTrans> listCustomerTrans = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "Select u.id, u.customer_id, u.tgl_transaksi, ur.customer_name FROM [Customer_Trans] u JOIN [Customer] ur " +
                        "ON (u.deleted is null OR u.deleted = '0') AND (ur.deleted is null OR ur.deleted = '0') AND u.customer_id = ur.customer_id", con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CustomerTrans customerTrans = new CustomerTrans();
                            customerTrans.CustomerTransID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            customerTrans.CustomerID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            customerTrans.TransDate = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? Utility.Utility.DefaultDateTime() : reader.GetDateTime(2);
                            customerTrans.CustomerName = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);
                            

                            if (listCustomerTrans == null)
                                listCustomerTrans = new List<CustomerTrans>();

                            listCustomerTrans.Add(customerTrans);
                            customerTrans = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerTransRepository.cs - listCustomerTrans() " + e.Message);
            }

            return listCustomerTrans;
        }

        public bool DeleteCustomerTrans(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            try
            {
                using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();

                    SqlParameter tempSqlParam = sqlParam[0];
                    using (SqlCommand command = new SqlCommand(
                        "Update [Customer_Trans_Detail] set deleted = '1' WHERE customer_trans_id = " + sqlParam[0].ParameterName, con))
                    {
                        command.Transaction = sqlTransaction;
                        command.Parameters.Add(sqlParam[0]);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();

                        using (SqlCommand subCommand = new SqlCommand(
                            "Update [Customer_Trans] set deleted = '1' WHERE id = " + sqlParam[0].ParameterName, con
                            ))
                        {
                            subCommand.Transaction = sqlTransaction;
                            subCommand.Parameters.Add(sqlParam[0]);
                            n = subCommand.ExecuteNonQuery();
                        }
                    }

                    if (n > 0)
                        sqlTransaction.Commit();
                    else
                        sqlTransaction.Rollback();
                }
            }
            catch (Exception e)
            {
                if (sqlTransaction != null)
                    sqlTransaction.Rollback();

                Logging.Error("SqlCustomerTransRepository.cs - DeleteCustomerTrans() " + e.Message);
            }
            finally
            {
                sqlTransaction.Dispose();
            }

            return n > 0;
        }

        public List<CustomerTransDetail> ListCustomerTransDetail(Guid ID)
        {
            List<CustomerTransDetail> listCustomerTransDetail = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "Select ctd.id, ctd.customer_trans_id, ctd.type_id, ctd.pelayaran_id, ctd.origin, " + 
                        "ctd.destination, ctd.price, ctd.condition_id, ctd.no_seal, " +
                        "(Select Top 1 type_name From [Type_Cont] tc Where tc.type_id = ctd.type_id) as type_name, " + 
                        "(Select Top 1 name From [Pelayaran] p Where p.pelayaran_id = ctd.pelayaran_id) as pelayaran_name, " +
                        "(Select Top 1 city_name From [City] c Where c.city_id = ctd.origin) as origin, " +
                        "(Select Top 1 city_name From [City] c Where c.city_id = ctd.destination) as destination, " + 
                        "(Select Top 1 condition_name From [Condition] c Where c.condition_id = ctd.condition_id) as condition " + 
                        "FROM [Customer_Trans_Detail] ctd " +
                        "Where (ctd.deleted is null OR ctd.deleted = '0') " +
                        "AND ctd.customer_trans_id = '" + ID + "'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CustomerTransDetail customerTransDetail = new CustomerTransDetail();
                            customerTransDetail.CustomerDetailTransID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            customerTransDetail.CustomerTransID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            customerTransDetail.TypeID = Utility.Utility.ConvertToUUID(reader.GetValue(2).ToString());
                            customerTransDetail.PelayaranID = Utility.Utility.ConvertToUUID(reader.GetValue(3).ToString());
                            customerTransDetail.Origin = Utility.Utility.ConvertToUUID(reader.GetValue(4).ToString());
                            customerTransDetail.Destination = Utility.Utility.ConvertToUUID(reader.GetValue(5).ToString()); ;
                            customerTransDetail.Price = reader.GetDecimal(6);
                            customerTransDetail.ConditionID = Utility.Utility.ConvertToUUID(reader.GetValue(7).ToString()); ;
                            customerTransDetail.NoSeal = reader.GetString(8);

                            customerTransDetail.TypeName = reader.GetString(9);
                            customerTransDetail.PelayaranName = reader.GetString(10);
                            customerTransDetail.OriginName = reader.GetString(11);
                            customerTransDetail.DestinationName = reader.GetString(12);
                            customerTransDetail.ConditionName = reader.GetString(13);

                            if (listCustomerTransDetail == null)
                                listCustomerTransDetail = new List<CustomerTransDetail>();

                            listCustomerTransDetail.Add(customerTransDetail);
                            customerTransDetail = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerTransRepository.cs - ListCustomerTransDetail() " + e.Message);
            }

            return listCustomerTransDetail;
        }

        public bool CreateCustomerTrans(SqlParameter[] sqlParamInsert, SqlParameter[] sqlParamMaster)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            try
            {
                using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();

                    using (SqlCommand masterCommand = new SqlCommand(
                        "INSERT INTO [customer_trans] VALUES(" + sqlParamMaster[0].ParameterName + ", " + 
                        sqlParamMaster[1].ParameterName + ", " + 
                        sqlParamMaster[2].ParameterName + ", " + 
                        sqlParamMaster[3].ParameterName + 
                        ")", 
                        con
                        )) 
                        {
                            masterCommand.Transaction = sqlTransaction;

                            for (int i = 0; i < sqlParamMaster.Length; i++)
                                masterCommand.Parameters.Add(sqlParamMaster[i]);
                            n = masterCommand.ExecuteNonQuery();
                    
                            if(n > 0){
                                int k = 0;
                                int value = 0;
                                for(int i=0; i<sqlParamInsert.Length/10; i++){
                                    n = 0;
                                    using (SqlCommand command = new SqlCommand(
                                        "Insert into [Customer_Trans_Detail] values (" +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName +
                                        ")", con))
                                    {
                                        command.Transaction = sqlTransaction;

                                        for (int j = 0; j < 10; j++)
                                            command.Parameters.Add(sqlParamInsert[value++]);
                                        n = command.ExecuteNonQuery();
                                        command.Parameters.Clear();

                                        if(n == 0)
                                            break;
                                    }
                                }
                            }
                        }

                    if (n > 0)
                        sqlTransaction.Commit();
                    else
                        sqlTransaction.Rollback();
                }
            }
            catch (Exception e)
            {
                if (sqlTransaction != null)
                    sqlTransaction.Rollback();

                Logging.Error("SqlCustomerTransRepository.cs - CreateCustomerTrans() " + e.Message);
            }
            finally
            {
                sqlTransaction.Dispose();
            }

            return n > 0;
        }


        public bool EditCustomerTrans(SqlParameter[] sqlParamInsert, SqlParameter[] sqlParamMaster)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            try
            {
                using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();

                    using (SqlCommand deletedCommand = new SqlCommand(
                        "Delete [Customer_Trans_Detail] WHERE customer_trans_id = " + sqlParamMaster[0].ParameterName,
                        con
                        ))
                    {
                        deletedCommand.Transaction = sqlTransaction;
                        deletedCommand.Parameters.Add(sqlParamMaster[0]);
                        n = deletedCommand.ExecuteNonQuery();
                        deletedCommand.Parameters.Clear();

                        if (n > 0)
                        {
                            n = 0;

                            using (SqlCommand masterCommand = new SqlCommand(
                                "Update [customer_trans] set customer_id = " + sqlParamMaster[1].ParameterName + " WHERE id = " + sqlParamMaster[0].ParameterName,
                                con
                                ))
                            {
                                masterCommand.Transaction = sqlTransaction;
                                masterCommand.Parameters.Add(sqlParamMaster[1]);
                                masterCommand.Parameters.Add(sqlParamMaster[0]);
                                n = masterCommand.ExecuteNonQuery();

                                if (n > 0)
                                {
                                    int k = 0;
                                    int value = 0;
                                    for (int i = 0; i < sqlParamInsert.Length / 10; i++)
                                    {
                                        n = 0;
                                        using (SqlCommand command = new SqlCommand(
                                            "Insert into [Customer_Trans_Detail] values (" +
                                            sqlParamInsert[k++].ParameterName + ", " +
                                            sqlParamInsert[k++].ParameterName + ", " +
                                            sqlParamInsert[k++].ParameterName + ", " +
                                            sqlParamInsert[k++].ParameterName + ", " +
                                            sqlParamInsert[k++].ParameterName + ", " +
                                            sqlParamInsert[k++].ParameterName + ", " +
                                            sqlParamInsert[k++].ParameterName + ", " +
                                            sqlParamInsert[k++].ParameterName + ", " +
                                            sqlParamInsert[k++].ParameterName + ", " +
                                            sqlParamInsert[k++].ParameterName +
                                            ")", con))
                                        {
                                            command.Transaction = sqlTransaction;

                                            for (int j = 0; j < 10; j++)
                                                command.Parameters.Add(sqlParamInsert[value++]);
                                            n = command.ExecuteNonQuery();
                                            command.Parameters.Clear();

                                            if (n == 0)
                                                break;
                                        }
                                    }
                                }
                            }

                            if (n > 0)
                                sqlTransaction.Commit();
                            else
                                sqlTransaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (sqlTransaction != null)
                    sqlTransaction.Rollback();

                Logging.Error("SqlCustomerTransRepository.cs - EditCustomerTrans() " + e.Message);
            }
            finally
            {
                sqlTransaction.Dispose();
            }

            return n > 0;
        }
    }
}