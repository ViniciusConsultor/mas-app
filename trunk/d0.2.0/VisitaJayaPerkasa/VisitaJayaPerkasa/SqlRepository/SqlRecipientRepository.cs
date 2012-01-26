using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlRecipientRepository
    {
        public List<Recipient> GetRecipient(){
            List<Recipient> listRecipient = null;

                try
                {
                    using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                    {
                        con.Open();

                        using (SqlCommand command = new SqlCommand(
                            "SELECT recipient_id, recipient_name, supplier_id, " + 
                            "(SELECT supplier_name FROM [SUPPLIER] s WHERE s.supplier_id = r.supplier_id) as SupplierName " + 
                            "FROM [Recipient] r " +
                            "WHERE (r.deleted is null OR r.deleted = '0') " +  
                            "ORDER BY recipient_name ASC"
                            , con))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Recipient recipient= new Recipient();
                                recipient.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                                recipient.Name = reader.GetString(1);
                                recipient.SupplierID = Utility.Utility.ConvertToUUID(reader.GetValue(2).ToString());
                                recipient.SupplierName = reader.GetString(3);

                                if (listRecipient == null)
                                    listRecipient = new List<Recipient>();

                                listRecipient.Add(recipient);
                                recipient = null;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logging.Error("SqlRecipientRepository.cs - GetRecipient() " + e.Message);
                }

                return listRecipient;
        }

        public Guid GetRecipientIDbyRecipientName(String recipientName , Guid supplierID)
        {
            Guid ID = Guid.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 recipient_id FROM [RECIPIENT] WHERE recipient_name = '" + recipientName + "' AND supplier_id = '" + supplierID + "' AND deleted = '1'", con))
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

                Logging.Error("SqlRecipientRepository.cs - GetRecipientIDbyRecipientName() " + e.Message);
            }

            return ID;
        }

        public bool DeleteRecipient(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(
                        "Update [Recipient] set deleted = '1' WHERE recipient_id = " + sqlParam[0].ParameterName, con))
                    {
                        command.Transaction = sqlTransaction;
                        command.Parameters.Add(sqlParam[0]);
                        n = command.ExecuteNonQuery();
                    }

                    if (n > 0)
                        sqlTransaction.Commit();
                    else
                        sqlTransaction.Rollback();
                }
                catch (Exception e)
                {
                    if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    Logging.Error("SqlRecipientRepository.cs - DeleteRecipient() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


        public bool CheckRecipient(SqlParameter[] sqlParam)
        {
            bool exists = false;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();  
                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 recipient_name FROM [Recipient] WHERE recipient_name = " + sqlParam[0].ParameterName, con))
                    {
                        foreach (SqlParameter tempSqlParam in sqlParam)
                            command.Parameters.Add(tempSqlParam);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            exists = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Logging.Error("SqlRecipientRepository.cs - CheckRecipient() " + e.Message);
            }

            return exists;
        }

        public bool CheckRecipientName(SqlParameter[] sqlParam, Guid gID, bool checkDeletedData = false)
        {
            bool exists = false;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();
                    String criteria = "";
                    if (!gID.ToString().Equals(Guid.Empty.ToString()))
                        criteria = " AND recipient_id != '" + gID.ToString() + "'";
                    else if (checkDeletedData)
                        criteria = " AND deleted = '1'";
                    else
                        criteria = " AND (deleted is null OR deleted = '0')";

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 recipient_name FROM [RECIPIENT] WHERE recipient_name = '" + sqlParam[0].Value + "' AND supplier_id = '" + Utility.Utility.ConvertToUUID(sqlParam[1].Value.ToString()) + "'" + criteria, con))
                    {
                        //foreach (SqlParameter tempSqlParam in sqlParam)
                        //    command.Parameters.Add(tempSqlParam);

                        SqlDataReader reader = command.ExecuteReader();
                        command.Parameters.Clear();
                        while (reader.Read())
                        {
                            exists = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Logging.Error("SqlRecipientRepository.cs - CheckRecipientName() " + e.Message);
            }

            return exists;
        }

        public bool ActivateRecipient(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;
            Guid ID = GetRecipientIDbyRecipientName(sqlParam[1].Value.ToString(), Utility.Utility.ConvertToUUID(sqlParam[2].Value.ToString()));


            if (ID.ToString().Equals(Guid.Empty.ToString()))
                return false;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();

                    using (SqlCommand command = new SqlCommand(
                        "Update [RECIPIENT] set " +
                        "recipient_name = " + sqlParam[1].ParameterName + ", " +
                        "supplier_id = " + sqlParam[2].ParameterName + ", " +
                        "deleted = " + sqlParam[3].ParameterName + " " +
                        "WHERE recipient_id = '" + ID + "'"
                        , con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 1; i < 4; i++)
                            command.Parameters.Add(sqlParam[i]);
                        n = command.ExecuteNonQuery();
                    }

                    if (n > 0)
                        sqlTransaction.Commit();
                    else
                        sqlTransaction.Rollback();
                }
                catch (Exception e)
                {
                    if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    Logging.Error("SqlRecipientRepository.cs - ActivateRecipient() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


        public bool CreateRecipient(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(
                        "Insert into [Recipient] values (" +
                        sqlParam[0].ParameterName + ", " +
                        sqlParam[1].ParameterName + ", " +
                        sqlParam[2].ParameterName + ", " +
                        sqlParam[3].ParameterName +
                        ")", con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 0; i < sqlParam.Length; i++)
                            command.Parameters.Add(sqlParam[i]);
                        n = command.ExecuteNonQuery();
                    }

                    if (n > 0)
                        sqlTransaction.Commit();
                    else
                        sqlTransaction.Rollback();
                }
                catch (Exception e)
                {
                    if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    Logging.Error("SqlRecipientRepository.cs - CheckRecipient() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


        public bool EditRecipient(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(
                        "Update [Recipient] set recipient_name = " + sqlParam[0].ParameterName +
                        ", supplier_id = " + sqlParam[1].ParameterName +
                        " WHERE recipient_id = " + sqlParam[2].ParameterName, con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 0; i < sqlParam.Length; i++)
                            command.Parameters.Add(sqlParam[i]);
                        n = command.ExecuteNonQuery();
                    }

                    if (n > 0)
                        sqlTransaction.Commit();
                    else
                        sqlTransaction.Rollback();
                }
                catch (Exception e)
                {
                    if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    Logging.Error("SqlRecipientRepository.cs - EditRecipient() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

    }
}
