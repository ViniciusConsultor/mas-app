using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;
using VisitaJayaPerkasa.Entities;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlWareHouseRepository
    {
        public List<VisitaJayaPerkasa.Entities.WareHouse> GetWareHouse()
        {
            List<VisitaJayaPerkasa.Entities.WareHouse> listWareHouse = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT stuffing_place_id, address, phone, fax, email, contact_person FROM [WAREHOUSE] WHERE (deleted is null OR deleted = '0') " +
                        "ORDER BY address ASC"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            VisitaJayaPerkasa.Entities.WareHouse warehouse = new VisitaJayaPerkasa.Entities.WareHouse();
                            warehouse.Id = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            warehouse.Address = reader.GetString(1);
                            warehouse.Phone = Utility.Utility.IsDBNull(reader.GetValue(2)) ? null : reader.GetString(2);
                            warehouse.Fax = Utility.Utility.IsDBNull(reader.GetValue(3)) ? null : reader.GetString(3);
                            warehouse.Email = Utility.Utility.IsDBNull(reader.GetValue(4)) ? null : reader.GetString(4);
                            warehouse.ContactPerson = Utility.Utility.IsDBNull(reader.GetValue(5)) ? null : reader.GetString(5);

                            if (listWareHouse == null)
                                listWareHouse = new List<VisitaJayaPerkasa.Entities.WareHouse>();

                            listWareHouse.Add(warehouse);
                            warehouse = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlWareHouseRepository.cs - GetWareHouse() " + e.Message);
            }

            return listWareHouse;
        }

        public bool CheckWarehouse(SqlParameter[] sqlParam, Guid gID, bool checkDeletedData = false)
        {
            bool exists = false;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();
                    String criteria = "";
                    if (!gID.ToString().Equals(Guid.Empty.ToString()))
                        criteria = " AND stuffing_place_id != '" + gID.ToString() + "'";
                    else if (checkDeletedData)
                        criteria = " AND deleted = '1'";
                    else
                        criteria = " AND (deleted is null OR deleted = '0')";

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 address FROM [WAREHOUSE] WHERE address = '" + sqlParam[0].Value + "'" + /* AND email = '" + sqlParam[1].Value + "' AND contact_person = '" + sqlParam[2].Value + "'" */  criteria, con))
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

                Logging.Error("SqlWareHouseRepository.cs - CheckWarehouse() " + e.Message);
            }

            return exists;
        }

        public bool ActivateWarehouse(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;
            Guid ID = GetWarehouseID(sqlParam[1].Value.ToString()/*, sqlParam[4].Value.ToString(), sqlParam[5].Value.ToString()*/);


            if (ID.ToString().Equals(Guid.Empty.ToString()))
                return false;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();

                    using (SqlCommand command = new SqlCommand(
                        "Update [WAREHOUSE] set " +
                        "address = " + sqlParam[1].ParameterName + ", " +
                        "phone = " + sqlParam[2].ParameterName + ", " +
                        "fax = " + sqlParam[3].ParameterName + ", " +
                        "email = " + sqlParam[4].ParameterName + ", " +
                        "contact_person = " + sqlParam[5].ParameterName + ", " +
                        "deleted = " + sqlParam[6].ParameterName + " " +
                        "WHERE stuffing_place_id = '" + ID + "'"
                        , con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 1; i < 7; i++)
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

                    Logging.Error("SqlWareHouseRepository.cs - ActivateWarehouse() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public Guid GetWarehouseID(String address)
        {
            Guid ID = Guid.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 stuffing_place_id FROM [WAREHOUSE] WHERE address = '" + address /*+ "' AND email = '" + email + "' AND contact_person = '" +  contactPerson + */ + "' AND deleted = '1'", con)) 
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

                Logging.Error("SqlWareHouseRepository.cs - GetWarehouseID() " + e.Message);
            }

            return ID;
        }

        public bool DeleteWareHouse(SqlParameter[] sqlParam)
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
                        "Update [Warehouse] set deleted = '1' WHERE stuffing_place_id = " + sqlParam[0].ParameterName, con))
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

                    Logging.Error("SqlWareHouseRepository.cs - DeleteWareHouse() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public bool CreateWareHouse(SqlParameter[] sqlParam)
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
                        "Insert into [Warehouse] values (" +
                        sqlParam[0].ParameterName + ", " +
                        sqlParam[1].ParameterName + ", " +
                        sqlParam[2].ParameterName + ", " +
                        sqlParam[3].ParameterName + ", " +
                        sqlParam[4].ParameterName + ", " +
                        sqlParam[5].ParameterName + ", " +
                        sqlParam[6].ParameterName +
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

                    Logging.Error("SqlWareHouseRepository.cs - CreateWareHouse() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


        public bool EditWareHouse(SqlParameter[] sqlParam)
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
                        "Update [WAREHOUSE] set address = " + sqlParam[0].ParameterName +
                        ", phone = " + sqlParam[1].ParameterName +
                        ", fax = " + sqlParam[2].ParameterName +
                        ", email = " + sqlParam[3].ParameterName +
                        ", contact_person = " + sqlParam[4].ParameterName +
                        " WHERE stuffing_place_id = " + sqlParam[5].ParameterName, con))
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

                    Logging.Error("SqlWareHouseRepository.cs - EditWareHouse() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public bool CheckWarehouseAddress(SqlParameter[] sqlParam, bool excludeDeleted = true, bool isEdit = false)
        {
            bool exists = false;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    string conditional = "";
                    if (isEdit)
                        conditional = " AND stuffing_place_id != " + sqlParam[0].ParameterName;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 address FROM [WAREHOUSE] WHERE address = " + sqlParam[1].ParameterName + conditional, con))
                    {
                        foreach (SqlParameter tempSqlParam in sqlParam)
                            command.Parameters.Add(tempSqlParam);

                        SqlDataReader reader = command.ExecuteReader();
                        command.Parameters.Clear();
                        while (reader.Read())
                        {
                            if (sqlParam[1].SqlValue.ToString().Equals((reader.GetValue(0).ToString()), StringComparison.CurrentCultureIgnoreCase))
                                exists = true;
                            else
                                exists = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Logging.Error("SqlWareHouseRepository.cs - CheckWarehouseAddress() " + e.Message);
            }

            return exists;
        }
    }
}
