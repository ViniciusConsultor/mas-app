﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlTypeContRepository
    {
        public List<VisitaJayaPerkasa.Entities.TypeCont> GetTypeCont()
        {
            List<VisitaJayaPerkasa.Entities.TypeCont> listTypeCont = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT type_id, type_code, type_name FROM [Type_Cont] WHERE (deleted is null OR deleted = '0') " +
                        "ORDER BY type_code ASC, type_name ASC"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            VisitaJayaPerkasa.Entities.TypeCont typeCont= new VisitaJayaPerkasa.Entities.TypeCont();
                            typeCont.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            typeCont.TypeCode = reader.GetString(1);
                            typeCont.TypeName = reader.GetString(2);

                            if (listTypeCont == null)
                                listTypeCont = new List<VisitaJayaPerkasa.Entities.TypeCont>();

                            listTypeCont.Add(typeCont);
                            typeCont = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlTypeContRepository.cs - GetTypeCont() " + e.Message);
            }

            return listTypeCont;
        }

        public bool CheckTypeCont(SqlParameter[] sqlParam, Guid gID, bool checkDeletedData = false)
        {
            bool exists = false;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    String criteria = "";
                    if (!gID.ToString().Equals(Guid.Empty.ToString()))
                        criteria = " AND type_id != '" + gID.ToString() + "'";
                    else if (checkDeletedData)
                        criteria = " AND deleted = '1'";
                    else
                        criteria = " AND (deleted is null OR deleted = '0')";

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 type_code FROM [TYPE_CONT] WHERE type_code = '" + sqlParam[0].Value + "' AND type_name = '" + sqlParam[1].Value + "'" + criteria, con))
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

                Logging.Error("SqlTypeContRepository.cs - CheckTypeCont() " + e.Message);
            }

            return exists;
        }

        public bool ActivateTypeCont(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;
            Guid ID = GetTypeContID(sqlParam[1].Value.ToString(), sqlParam[2].Value.ToString());


            if (ID.ToString().Equals(Guid.Empty.ToString()))
                return false;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    sqlTransaction = con.BeginTransaction();

                    using (SqlCommand command = new SqlCommand(
                        "Update [TYPE_CONT] set " +
                        "type_code = " + sqlParam[1].ParameterName + ", " +
                        "type_name = " + sqlParam[2].ParameterName + ", " +
                        "deleted = " + sqlParam[3].ParameterName + " " +
                        "WHERE type_id = '" + ID + "'"
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

                    Logging.Error("SqlTypeContRepository.cs - ActivateTypeCont() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public Guid GetTypeContID(String code, String name)
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
                        "SELECT TOP 1 type_id FROM [TYPE_CONT] WHERE type_code = '" + code + "' AND type_name = '" + name + "' AND deleted = '1'", con))
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

                Logging.Error("SqlTypeContRepository.cs - GetTypeContID() " + e.Message);
            }

            return ID;
        }

        public bool DeleteTypeCont(SqlParameter[] sqlParam)
        {
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
                        "Update [Type_Cont] set deleted = '1' WHERE type_id = " + sqlParam[0].ParameterName, con))
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

                    Logging.Error("SqlTypeContRepository.cs - DeleteTypeCont() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public bool CheckTypeCont(SqlParameter[] sqlParam)
        {
            bool exists = false;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 type_id FROM [Type_Cont] WHERE type_code = " + sqlParam[0].ParameterName, con))
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

                Logging.Error("SqlTypeContRepository.cs - CheckTypeCont() " + e.Message);
            }

            return exists;
        }


        public bool CreateTypeCont(SqlParameter[] sqlParam)
        {
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
                        "Insert into [Type_Cont] values (" +
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

                    Logging.Error("SqlTypeContRepository.cs - CreateTypeCont() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        
        public bool EditTypeCont(SqlParameter[] sqlParam)
        {
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
                        "Update [Type_Cont] set " + 
                        "type_code = " + sqlParam[1].ParameterName + ", " + 
                        "type_name = " + sqlParam[2].ParameterName + ", " + 
                        "deleted = " + sqlParam[3].ParameterName + 
                        " WHERE type_id = " + sqlParam[0].ParameterName, con))
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

                    Logging.Error("SqlTypeContRepository.cs - EditTypeCont() " + e.Message);
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
