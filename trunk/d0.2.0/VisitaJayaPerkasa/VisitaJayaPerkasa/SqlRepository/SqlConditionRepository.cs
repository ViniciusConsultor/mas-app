using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlConditionRepository
    {
        public List<Condition> GetConditions()
        {
            List<Condition> listCondition = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT condition_id, condition_code, condition_name FROM [Condition] " +
                        "WHERE (deleted is null OR deleted = '0') " +
                        "ORDER BY condition_name ASC"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Condition condition = new Condition();
                            condition.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            condition.ConditionCode = reader.GetString(1);
                            condition.ConditionName = reader.GetString(2);

                            if (listCondition == null)
                                listCondition = new List<Condition>();

                            listCondition.Add(condition);
                            condition = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlConditionRepository.cs - GetConditions() " + e.Message);
            }

            return listCondition;
        }


        public string GetConditionIDByCode(string code)
        {
            string ID = Guid.Empty.ToString();

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT condition_id FROM [Condition] WHERE condition_code = '" + code + "'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ID = reader.GetGuid(0).ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlConditionRepository.cs - GetConditionIDByCode() " + e.Message);
            }

            return ID;
        }

        public bool CheckConditionCode(SqlParameter[] sqlParam, Guid ID, bool checkDeletedData = false)
        {
            bool exists = false;
            String criteria;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    if (!ID.ToString().Equals(Guid.Empty.ToString()))
                        criteria = " AND condition_id != '" + ID.ToString() + "'";
                    else if (checkDeletedData)
                        criteria = " AND deleted = '1'";
                    else
                        criteria = " AND (deleted is null OR deleted = '0')";

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 condition_id FROM [Condition] WHERE condition_code = " + sqlParam[0].ParameterName + criteria, con))
                    {
                        foreach (SqlParameter tempSqlParam in sqlParam)
                            command.Parameters.Add(tempSqlParam);

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

                Logging.Error("SqlConditionRepository.cs - CheckConditionCode() " + e.Message);
            }

            return exists;
        }

        public bool CreateCondition(SqlParameter[] sqlParam)
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
                        "Insert into [Condition] values (" +
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

                    Logging.Error("SqlConditionRepository.cs - CreateCondition() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


        public bool ActivateCondition(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            string ID = GetConditionIDByCode(sqlParam[1].Value.ToString());
            if (ID.Equals(Guid.Empty.ToString()))
                return false;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(
                        "Update [Condition] set " + 
                        "condition_name = " + sqlParam[2].ParameterName + ", " +
                        "deleted = " + sqlParam[3].ParameterName +
                        " WHERE condition_id = '" + ID + "'" , con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 2; i < sqlParam.Length; i++)
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

                    Logging.Error("SqlConditionRepository.cs - ActivateCondition() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


        public bool EditCondition(SqlParameter[] sqlParam)
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
                        "Update [Condition] set condition_code = " + sqlParam[0].ParameterName +
                        ", condition_name = " + sqlParam[1].ParameterName +
                        " WHERE condition_id = " + sqlParam[2].ParameterName, con))
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

                    Logging.Error("SqlConditionRepository.cs - EditCondition() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public bool DeleteCondition(SqlParameter[] sqlParam)
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
                        "Update [Condition] set deleted = '1' WHERE condition_id = " + sqlParam[0].ParameterName, con))
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

                    Logging.Error("SqlConditionRepository.cs - DeleteCondition() " + e.Message);
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
