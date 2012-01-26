using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlCityRepository
    {
        public List<City> GetCity()
        {
            List<City> listCity = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT city_id, city_code, city_name, days FROM [City] " +
                        "WHERE (deleted is null OR deleted = '0') " +
                        "ORDER BY city_name ASC"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            City city = new City();
                            city.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            city.CityCode = reader.GetString(1);
                            city.CityName = reader.GetString(2);
                            city.Days = reader.GetInt32(3);

                            if (listCity == null)
                                listCity = new List<City>();

                            listCity.Add(city);
                            city = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCityRepository.cs - GetCity() " + e.Message);
            }

            return listCity;
        }



        public bool CheckCityCode(SqlParameter[] sqlParam, Guid ID, bool checkDeletedData = false)
        {
            bool exists = false;
            String criteria;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    if (!ID.ToString().Equals(Guid.Empty.ToString()))
                        criteria = " AND city_id != '" + ID.ToString() + "'";
                    else if (checkDeletedData)
                        criteria = " AND deleted = '1'";
                    else
                        criteria = " AND (deleted is null OR deleted = '0')";


                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 city_id FROM [City] WHERE city_code = " + sqlParam[0].ParameterName + criteria, con))
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

                Logging.Error("SqlCityRepository.cs - CheckCityCode() " + e.Message);
            }

            return exists;
        }

        public bool CreateCity(SqlParameter[] sqlParam)
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
                        "Insert into [City] values (" +
                        sqlParam[0].ParameterName + ", " +
                        sqlParam[1].ParameterName + ", " +
                        sqlParam[2].ParameterName + ", " +
                        sqlParam[3].ParameterName + ", " +
                        sqlParam[4].ParameterName +
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

                    Logging.Error("SqlCityRepository.cs - CreateCity() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }
            return n > 0;
        }

        public bool EditCity(SqlParameter[] sqlParam)
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
                        "Update [City] set city_code = " + sqlParam[0].ParameterName +
                        ", city_name = " + sqlParam[1].ParameterName +
                        ", days = " + sqlParam[2].ParameterName +
                        " WHERE city_id = " + sqlParam[3].ParameterName, con))
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

                    Logging.Error("SqlCityRepository.cs - EditCity() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


        public bool ActivateCity(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            String CityID = GetCityByCityCode(sqlParam[1].Value.ToString());
            if(CityID.Equals(Guid.Empty.ToString()))
                return false;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(
                        "Update [City] set " +
                        "city_name = " + sqlParam[2].ParameterName +
                        ", days = " + sqlParam[3].ParameterName +
                        ", deleted = " + sqlParam[4].ParameterName + 
                        " WHERE city_id = '" + CityID + "'" , con))
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

                    Logging.Error("SqlCityRepository.cs - ActivateCity() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public string GetCityByCityCode(String cityCode) { 
            string CityID = Guid.Empty.ToString();

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 city_id FROM [City] " +
                        "WHERE city_code = '" + cityCode + "'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CityID = reader.GetGuid(0).ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCityRepository.cs - GetCityByCityCode() " + e.Message);
            }

            return CityID;
        }

        public bool DeleteCity(SqlParameter[] sqlParam)
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
                        "Update [City] set deleted = '1' WHERE city_id = " + sqlParam[0].ParameterName, con))
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

                    Logging.Error("SqlCityRepository.cs - DeleteCity() " + e.Message);
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
