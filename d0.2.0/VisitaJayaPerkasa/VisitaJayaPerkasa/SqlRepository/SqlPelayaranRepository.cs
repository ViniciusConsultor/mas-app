using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Entities;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlPelayaranRepository
    {
        public List<Pelayaran> GetPelayaran()
        {
            List<Pelayaran> listPelayaran = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT pelayaran_id, pelayaran.supplier_id, supplier.supplier_name as name FROM [Pelayaran], [supplier] " + 
                        "WHERE (pelayaran.deleted is null OR pelayaran.deleted = '0') " +
                        "and ([supplier].deleted is null OR [supplier].deleted = '0') " +
                        "and [supplier].supplier_id = pelayaran.supplier_id " + 
                        "ORDER BY name ASC"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Pelayaran pelayaran = new Pelayaran();
                            pelayaran.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            pelayaran.supplierID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            pelayaran.supplierName = reader.GetString(2);

                            if (listPelayaran == null)
                                listPelayaran = new List<Pelayaran>();

                            listPelayaran.Add(pelayaran);
                            pelayaran = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPelayaranRepository.cs - GetPelayaran() " + e.Message);
            }

            return listPelayaran;
        }

        public List<PelayaranDetail> GetVessels()
        {
            List<PelayaranDetail> listVessel = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT DISTINCT pd.vessel_code, pd.vessel_name, pd.status_pinjaman, s.supplier_name, pd.pelayaran_detail_id FROM [Pelayaran_Detail] pd " +
                        "INNER JOIN [Pelayaran] p ON p.pelayaran_id = pd.pelayaran_id  " +
                        "INNER JOIN [Supplier] s ON s.supplier_id = p.supplier_id " +
                        "WHERE (pd.deleted is null OR pd.deleted = '0') " +
                        "ORDER BY pd.vessel_name ASC"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            PelayaranDetail pelayaranDetail = new PelayaranDetail();
                            pelayaranDetail.PelayaranDetailID = Guid.Parse(reader.GetValue(4).ToString());
                            pelayaranDetail.VesselCode = reader.GetValue(0).ToString();
                            pelayaranDetail.VesselName = (reader.GetBoolean(2)) ? (reader.GetString(1) + " - " + reader.GetString(3) + " [loan]") : reader.GetString(1) + " - " + reader.GetString(3);
                            pelayaranDetail.VesselCodeAndPelayaranID = pelayaranDetail.PelayaranDetailID + pelayaranDetail.VesselCode;

                            if (listVessel == null)
                                listVessel = new List<PelayaranDetail>();

                            listVessel.Add(pelayaranDetail);
                            pelayaranDetail = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPelayaranRepository.cs - GetVessels() " + e.Message);
            }

            return listVessel;
        }


        public List<PelayaranDetail> GetVessels(string destination, DateTime date)
        {
            List<PelayaranDetail> listVessel = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT DISTINCT pd.vessel_code, pd.vessel_name, pd.status_pinjaman, sup.supplier_name, pd.pelayaran_detail_id " +
                        "FROM [Pelayaran_Detail] pd " +
                        "INNER JOIN [PELAYARAN] p ON p.pelayaran_id = pd.pelayaran_id " +
                        "INNER JOIN [SCHEDULE] s ON s.pelayaran_detail_id = pd.pelayaran_detail_id " +
                        "INNER JOIN [SUPPLIER] sup ON p.supplier_id = sup.supplier_id " +
                        "WHERE (pd.deleted is null OR pd.deleted = '0') AND (sup.deleted is null OR sup.deleted = '0') " +
                        "AND cast(s.tgl_closing as date) >= cast('" + Utility.Utility.ConvertDateToString(date) + "' as date) " +
                        "AND (s.deleted is null OR s.deleted = '0') AND (p.deleted is null OR p.deleted = '0') " +
                        "AND s.ta is null " + 
                        "AND s.tujuan = '" + Utility.Utility.ConvertToUUID(destination) + "' " + 
                        "AND pd.vessel_code = s.vessel_code ", con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            PelayaranDetail pelayaranDetail = new PelayaranDetail();
                            pelayaranDetail.PelayaranDetailID = Guid.Parse(reader.GetValue(4).ToString());
                            pelayaranDetail.VesselCode = reader.GetValue(0).ToString();
                            pelayaranDetail.VesselName = (reader.GetBoolean(2)) ? (reader.GetString(1) + " - " + reader.GetString(3) + " [loan]") : reader.GetString(1) + " - " + reader.GetString(3);

                            if (listVessel == null)
                                listVessel = new List<PelayaranDetail>();

                            listVessel.Add(pelayaranDetail);
                            pelayaranDetail = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPelayaranRepository.cs - GetVessels() " + e.Message);
            }

            return listVessel;
        }

        public bool DeletePelayaran(SqlParameter[] sqlParam)
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
                        "Update [Pelayaran] set deleted = '1' WHERE pelayaran_id = " + sqlParam[0].ParameterName, con))
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

                    Logging.Error("SqlPelayaranRepository.cs - DeletePelayaran() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


        public bool CheckPelayaran(SqlParameter[] sqlParam, Guid ID, bool checkDeletedData = false)
        {
            bool exists = false;
            String criteria;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    if (!ID.ToString().Equals(Guid.Empty.ToString()))
                        criteria = " AND pelayaran_id != '" + ID.ToString() + "'";
                    else if (checkDeletedData)
                        criteria = " AND deleted = '1'";
                    else
                        criteria = " AND (deleted is null OR deleted = '0')";


                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 pelayaran_id FROM [Pelayaran] WHERE supplier_id = " + sqlParam[1].ParameterName +
                        criteria, con))
                    {
                        command.Parameters.Add(sqlParam[1]);

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
                Logging.Error("SqlPelayaranRepository.cs - CheckPelayaran() " + e.Message);
            }

            return exists;
        }

        public Pelayaran CheckPelayaranForEdit(SqlParameter[] sqlParam)
        {
            Pelayaran pelayaran = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 pelayaran_id, pelayaran.supplier_id, supplier.supplier_name FROM [Pelayaran], [supplier] " + 
                        "WHERE pelayaran.supplier_id = supplier.supplier_id and supplier.supplier_name = " + sqlParam[0].ParameterName, con))
                    {
                        foreach (SqlParameter tempSqlParam in sqlParam)
                            command.Parameters.Add(tempSqlParam);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            pelayaran = new Pelayaran();
                            pelayaran.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            pelayaran.supplierID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            pelayaran.supplierName = reader.GetString(2);
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Logging.Error("SqlPelayaranRepository.cs - CheckPelayaranForEdit() " + e.Message);
            }

            return pelayaran;
        }

        public bool CreatePelayaran(SqlParameter[] sqlParam)
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
                        "Insert into [Pelayaran] values (" +
                        sqlParam[0].ParameterName + ", " +
                        sqlParam[1].ParameterName + ", " +
                        sqlParam[2].ParameterName +
                        ")", con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 0; i < 3; i++)
                            command.Parameters.Add(sqlParam[i]);
                        n = command.ExecuteNonQuery();
                        command.Parameters.Clear();

                        int z = 3;
                        int subz = 3;
                        if ((n > 0) && sqlParam.Length > 3)
                        {
                            //-9 is total sqlparameter minus number of customer master
                            // / 8 is remain of total sqlparameter minus 9 is customer detail who have 8 number of field
                            for (int k = 0; k < ((sqlParam.Length - 3) / 6); k++)
                            {
                                using (SqlCommand subCommand = new SqlCommand(
                                    "Insert into [Pelayaran_Detail] values(" +
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

                                    for (int i = 0; i < 6; i++)
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
                catch (Exception e)
                {
                    if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    Logging.Error("SqlPelayaranRepository.cs - CreatePelayaran() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


        public bool EditPelayaran(SqlParameter[] sqlParam)
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

                    using (SqlCommand deleteCommand = new SqlCommand(
                        "Delete [Pelayaran_Detail] WHERE pelayaran_id = " + sqlParam[0].ParameterName, con))
                    {
                        deleteCommand.Transaction = sqlTransaction;
                        deleteCommand.Parameters.Add(sqlParam[0]);
                        n = deleteCommand.ExecuteNonQuery();
                        deleteCommand.Parameters.Clear();

                        using (SqlCommand command = new SqlCommand(
                            "Update [Pelayaran] set " +
                            "name = " + sqlParam[1].ParameterName + ", " +
                            "deleted = " + sqlParam[2].ParameterName + " WHERE " +
                            "pelayaran_id = " + sqlParam[0].ParameterName
                            , con))
                        {
                            command.Transaction = sqlTransaction;

                            for (int i = 0; i < 3; i++)
                                command.Parameters.Add(sqlParam[i]);
                            n = command.ExecuteNonQuery();
                            command.Parameters.Clear();

                            int z = 3;
                            int subz = 3;
                            if ((n > 0) && sqlParam.Length > 3)
                            {
                                //-9 is total sqlparameter minus number of customer master
                                // / 8 is remain of total sqlparameter minus 9 is customer detail who have 8 number of field
                                for (int k = 0; k < ((sqlParam.Length - 3) / 6); k++)
                                {
                                    using (SqlCommand subCommand = new SqlCommand(
                                        "Insert into [Pelayaran_Detail] values(" +
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

                                        for (int i = 0; i < 6; i++)
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

                    n = 0;
                    Logging.Error("SqlPelayaranRepository.cs - EditPelayaran() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public List<PelayaranDetail> ListPelayaranDetail(Guid ID)
        {
            List<PelayaranDetail> listPelayaranDetail = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT * FROM [Pelayaran_Detail] WHERE pelayaran_id = '" + ID + "' AND (deleted is null OR deleted = '0')"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            PelayaranDetail pelayaranDetail = new PelayaranDetail();
                            pelayaranDetail.PelayaranDetailID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            pelayaranDetail.ID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            pelayaranDetail.VesselCode = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);
                            pelayaranDetail.VesselName = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);
                            pelayaranDetail.StatusPinjaman = Convert.ToInt32(reader.GetBoolean(4));
                            if (pelayaranDetail.StatusPinjaman == 1)
                            {
                                pelayaranDetail.NamaStatusPinjaman = "Pinjaman";
                            }
                            else
                            {
                                pelayaranDetail.NamaStatusPinjaman = "Milik Sendiri";
                            }

                            if (listPelayaranDetail == null)
                                listPelayaranDetail = new List<PelayaranDetail>();

                            listPelayaranDetail.Add(pelayaranDetail);
                            pelayaranDetail = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPelayaranRepository.cs - ListPelayaranDetail() " + e.Message);
            }

            return listPelayaranDetail;
        }


        public bool ActivatePelayaran(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;
            string ID = GetPelayaranIDByName(sqlParam[1].Value.ToString());

            if (ID.Equals(Guid.Empty.ToString()))
                return false;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();

                    using (SqlCommand deleteCommand = new SqlCommand(
                        "Delete [Pelayaran_Detail] WHERE pelayaran_id = '" + ID + "'" , con))
                    {
                        deleteCommand.Transaction = sqlTransaction;
                        n = deleteCommand.ExecuteNonQuery();

                        using (SqlCommand command = new SqlCommand(
                            "Update [Pelayaran] set " +
                            "supplier_id = " + sqlParam[1].ParameterName + ", " +
                            "deleted = " + sqlParam[2].ParameterName + " WHERE " +
                            "pelayaran_id = '" + ID + "'"
                            , con))
                        {
                            command.Transaction = sqlTransaction;

                            for (int i = 1; i < 3; i++)
                                command.Parameters.Add(sqlParam[i]);
                            n = command.ExecuteNonQuery();
                            command.Parameters.Clear();

                            int z = 3;
                            int subz = 3;
                            if ((n > 0) && sqlParam.Length > 3)
                            {
                                //-9 is total sqlparameter minus number of customer master
                                // / 8 is remain of total sqlparameter minus 9 is customer detail who have 8 number of field
                                for (int k = 0; k < ((sqlParam.Length - 3) / 6); k++)
                                {
                                    using (SqlCommand subCommand = new SqlCommand(
                                        "Insert into [Pelayaran_Detail] values(" +
                                        sqlParam[(z+=2) - 2].ParameterName + ", " + //+2 for handle ID index on below
                                        "'" + ID + "', " + 
                                        sqlParam[z++].ParameterName + ", " +
                                        sqlParam[z++].ParameterName + ", " +
                                        sqlParam[z++].ParameterName + ", " +
                                        sqlParam[z++].ParameterName +
                                        ")"
                                        , con))
                                    {
                                        subCommand.Transaction = sqlTransaction;

                                        for (int i = 0; i < 6; i++)
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

                    n = 0;
                    Logging.Error("SqlPelayaranRepository.cs - ActivatePelayaran() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        
        public string GetPelayaranIDByName(string name)
        {
            string ID = Guid.Empty.ToString();

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT pelayaran_id FROM [Pelayaran] WHERE supplier_id = '" + name + "'"
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
                Logging.Error("SqlPelayaranRepository.cs - GetPelayaranIDByName() " + e.Message);
            }

            return ID;
        }

    }
}
