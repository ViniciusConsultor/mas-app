using System;
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
                        "Select u.id, u.customer_id, u.tgl_transaksi, ur.customer_name [Customer_Trans] u JOIN [Customer] ur " +
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
                        "Select ctd.id, ctd.customer_trans_id, ctd.type_id, ctd.pelayaran_id, ctd.origin, ctd.destination, ctd.price, ctd.condition_id, ctd.no_seal [Customer_Trans_Detail] ctd JOIN [Type_Cont] tc " +
                        "ON (ctd.deleted is null OR ctd.deleted = '0') AND (tc.deleted is null OR tc.deleted = '0') AND ctd.type_id = tc.type_id WHERE ctd.customer_trans_id = '" + ID + "' AND (ctd.deleted is null OR ctd.deleted = '0')"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CustomerTransDetail customerTransDetail = new CustomerTransDetail();
                            //customer.CustomerDetailID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            //customer.ID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            //customer.FirstName = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);
                            //customer.LastName = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);
                            //customer.CustomerDetailAddress = (Utility.Utility.IsDBNull(reader.GetValue(4))) ? null : reader.GetString(4);
                            //customer.CustomerDetailPhone = (Utility.Utility.IsDBNull(reader.GetValue(5))) ? null : reader.GetString(5);
                            //customer.CustomerDetailMobilePhone = (Utility.Utility.IsDBNull(reader.GetValue(6))) ? null : reader.GetString(6);

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

        public bool CreateCustomerTrans(SqlParameter[] sqlParamDeleted, SqlParameter[] sqlParamInsert)
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

                    if(sqlParamDeleted != null){
                        for (int i = 0; i < sqlParamDeleted.Length; i++)
                        {
                            using (SqlCommand deletedCommand = new SqlCommand(
                                "Delete [Customer_Trans_Detail] WHERE customer_trans_id = " + sqlParamDeleted[i].ParameterName
                                , con))
                            {
                                deletedCommand.Transaction = sqlTransaction;
                                deletedCommand.Parameters.Add(sqlParamDeleted[i]);
                                n = deletedCommand.ExecuteNonQuery();
                            }

                            if (n == 0)
                                break;
                        }
                    }

                    if (n > 0 || sqlParamDeleted == null) { 
                        
                    }



                    //di bawah lom 
                    using (SqlCommand command = new SqlCommand(
                        "Insert into [Customer_Trans] values (" +
                        sqlParam[0].ParameterName + ", " +
                        sqlParam[1].ParameterName + ", " +
                        sqlParam[2].ParameterName + ", " +
                        sqlParam[3].ParameterName +
                        ")", con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 0; i < 9; i++)
                            command.Parameters.Add(sqlParam[i]);
                        n = command.ExecuteNonQuery();
                        command.Parameters.Clear();

                        int z = 9;
                        int subz = 9;
                        if ((n > 0) && sqlParam.Length > 9)
                        {
                            //-9 is total sqlparameter minus number of customer master
                            // / 8 is remain of total sqlparameter minus 9 is customer detail who have 8 number of field
                            for (int k = 0; k < ((sqlParam.Length - 9) / 8); k++)
                            {
                                using (SqlCommand subCommand = new SqlCommand(
                                    "Insert into [Customer_Trans_Detail] values(" +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName +
                                    ")"
                                    , con))
                                {
                                    subCommand.Transaction = sqlTransaction;

                                    for (int i = 0; i < 8; i++)
                                        subCommand.Parameters.Add(sqlParam[subz++]);
                                    n = subCommand.ExecuteNonQuery();
                                    subCommand.Parameters.Clear();

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

        public bool EditCustomerTrans(SqlParameter[] sqlParamDeleted, SqlParameter[] sqlParamInsert)
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

                    using (SqlCommand deleteCommand = new SqlCommand(
                        "Delete [Customer_Trans_Detail] WHERE customer_trans_id = " + sqlParamDeleted[0].ParameterName, con))
                    {
                        deleteCommand.Transaction = sqlTransaction;
                        deleteCommand.Parameters.Add(sqlParamDeleted[0]);
                        n = deleteCommand.ExecuteNonQuery();
                        deleteCommand.Parameters.Clear();

                        using (SqlCommand command = new SqlCommand(
                            "Update [Customer_Trans] set " +
                            "customer_id = " + sqlParamInsert[1].ParameterName + ", " +
                            "tgl_transaksi = " + sqlParamInsert[2].ParameterName
                            , con))
                        {
                            command.Transaction = sqlTransaction;

                            for (int i = 0; i < 9; i++)
                                command.Parameters.Add(sqlParamInsert[i]);
                            n = command.ExecuteNonQuery();
                            command.Parameters.Clear();

                            int z = 9;
                            int subz = 9;
                            if ((n > 0) && sqlParamInsert.Length > 9)
                            {
                                //-9 is total sqlparameter minus number of customer master
                                // / 8 is remain of total sqlparameter minus 9 is customer detail who have 8 number of field
                                for (int k = 0; k < ((sqlParamInsert.Length - 9) / 8); k++)
                                {
                                    using (SqlCommand subCommand = new SqlCommand(
                                        "Insert into [Customer_Trans_Detail] values(" +
                                        sqlParamInsert[z++].ParameterName + ", " +
                                        sqlParamInsert[z++].ParameterName + ", " +
                                        sqlParamInsert[z++].ParameterName + ", " +
                                        sqlParamInsert[z++].ParameterName + ", " +
                                        sqlParamInsert[z++].ParameterName + ", " +
                                        sqlParamInsert[z++].ParameterName + ", " +
                                        sqlParamInsert[z++].ParameterName + ", " +
                                        sqlParamInsert[z++].ParameterName + ", " +
                                        sqlParamInsert[z++].ParameterName + ", " +
                                        sqlParamInsert[z++].ParameterName +
                                        ")"
                                        , con))
                                    {
                                        subCommand.Transaction = sqlTransaction;

                                        for (int i = 0; i < 8; i++)
                                            subCommand.Parameters.Add(sqlParamInsert[subz++]);
                                        n = subCommand.ExecuteNonQuery();
                                        subCommand.Parameters.Clear();

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
