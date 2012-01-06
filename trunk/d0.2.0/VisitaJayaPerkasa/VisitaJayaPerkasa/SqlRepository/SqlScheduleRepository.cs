using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlScheduleRepository
    {
        public List<Schedule> ListSchedule(string beginDate, string endDate, 
            string destination, string vessel, string voy)
        {
            List<Schedule> listSchedule = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT s.schedule_id, " + 
                        "s.tujuan, s.pelayaran_id, s.tgl_closing, s.voy, s.keterangan, s.vessel_code, " + 
                        "s.ro_begin_20, s.ro_begin_40, s.ro_end_20, s.ro_end_40, s.etd, s.td, s.eta, s.ta, s.unloading, " + 
                        "(SELECT TOP 1 city_name FROM [CITY] cc WHERE cc.city_id = s.tujuan) as tujuans, " +
                        "(SELECT TOP 1 name FROM [PELAYARAN] p WHERE p.pelayaran_id = s.pelayaran_id) as pelayarans, " +
                        "(SELECT TOP 1 vessel_name FROM [PELAYARAN_DETAIL] pd WHERE pd.pelayaran_id = s.pelayaran_id AND pd.vessel_code = s.vessel_code) as vessels, " +  
                        "(SELECT TOP 1 status_pinjaman FROM [PELAYARAN_DETAIL] pd WHERE pd.pelayaran_id = s.pelayaran_id AND pd.vessel_code = s.vessel_code) as status " + 
                        "FROM [Schedule] s, [PELAYARAN] p " +
                        "WHERE (p.deleted is null OR p.deleted = '0') AND s.tgl_closing > '" + beginDate + "' AND p.pelayaran_id = s.pelayaran_id AND " + 
                        "(s.etd > '" + beginDate + "' AND s.etd < '" + endDate + "') AND " + 
                        "s.voy like '%" + voy + "%' AND s.vessel_code like '%" + vessel + "%' AND " + 
                        "s.tujuan like '%" + destination + "%' AND (s.deleted is null OR s.deleted = '0')"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Schedule schedule = new Schedule();
                            schedule.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            schedule.tujuan = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            schedule.pelayaranID = Utility.Utility.ConvertToUUID(reader.GetValue(2).ToString());
                            schedule.tglclosing = reader.GetDateTime(3);
                            schedule.voy = (Utility.Utility.IsDBNull(reader.GetValue(4))) ? null : reader.GetString(4);
                            schedule.keterangan = (Utility.Utility.IsDBNull(reader.GetValue(5))) ? null : reader.GetString(5);
                            schedule.vesselCode = reader.GetString(6);
                            schedule.ro_begin_20 = (Utility.Utility.IsDBNull(reader.GetDecimal(7))) ? 0 : reader.GetInt32(7);
                            schedule.ro_begin_40 = (Utility.Utility.IsDBNull(reader.GetDecimal(8))) ? 0 : reader.GetInt32(8);
                            schedule.ro_end_20 = (Utility.Utility.IsDBNull(reader.GetDecimal(9))) ? 0 : reader.GetInt32(9);
                            schedule.ro_end_40 = (Utility.Utility.IsDBNull(reader.GetDecimal(10))) ? 0 : reader.GetInt32(10);
                            schedule.etd = reader.GetDateTime(11);
                            schedule.td = reader.GetDateTime(12);
                            schedule.eta = reader.GetDateTime(13);
                            schedule.ta = reader.GetDateTime(14);
                            schedule.unLoading = reader.GetDateTime(15);

                            schedule.berangkatTujuan = reader.GetString(16);
                            schedule.namaPelayaran = reader.GetString(17);
                            schedule.namaKapal = (reader.GetBoolean(19)) ? (reader.GetString(18) + " - loan") : reader.GetString(18);

                            if (listSchedule == null)
                                listSchedule = new List<Schedule>();

                            listSchedule.Add(schedule);
                            schedule = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlScheduleRepository.cs - ListSchedule() " + e.Message);
            }

            return listSchedule;
        }

        public bool DeleteSchedule(SqlParameter[] sqlParam)
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
                    using (SqlCommand command = new SqlCommand(
                        "Update [schedule] set deleted = '1' WHERE schedule_id = " + sqlParam[0].ParameterName, con))
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
            }
            catch (Exception e)
            {
                if (sqlTransaction != null)
                    sqlTransaction.Rollback();

                Logging.Error("SqlScheduleRepository.cs - DeleteSchedule() " + e.Message);
            }
            finally
            {
                sqlTransaction.Dispose();
            }

            return n > 0;
        }

        public bool CreateNewSchedule(SqlParameter[] sqlParam)
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
                    using (SqlCommand command = new SqlCommand(
                        "Insert into [Schedule] values (" +
                        sqlParam[0].ParameterName + ", " +
                        sqlParam[1].ParameterName + ", " +
                        sqlParam[2].ParameterName + ", " +
                        sqlParam[3].ParameterName + ", " +
                        sqlParam[4].ParameterName + ", " +
                        sqlParam[5].ParameterName + ", " +
                        sqlParam[6].ParameterName + ", " +
                        sqlParam[7].ParameterName + ", " +
                        sqlParam[8].ParameterName + ", " +
                        sqlParam[9].ParameterName + ", " +
                        sqlParam[10].ParameterName + ", " + 
                        sqlParam[11].ParameterName + ", " +
                        sqlParam[12].ParameterName + ", " +
                        sqlParam[13].ParameterName + ", " +
                        sqlParam[14].ParameterName + ", " +
                        sqlParam[15].ParameterName + ", " +
                        sqlParam[16].ParameterName +
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
            }
            catch (Exception e)
            {
                if (sqlTransaction != null)
                    sqlTransaction.Rollback();

                Logging.Error("SqlScheduleRepository.cs - CreateNewSchedule() " + e.Message);
            }
            finally
            {
                sqlTransaction.Dispose();
            }

            return n > 0;
        }

        public bool EditSchedule(SqlParameter[] sqlParam)
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
                    using (SqlCommand command = new SqlCommand(
                        "Update [Schedule] set berangkat = " + sqlParam[0].ParameterName + ", " +
                        "tujuan = " + sqlParam[1].ParameterName + ", " +
                        "pelayaran_id = " + sqlParam[2].ParameterName + ", " +
                        "vessel_id = " + sqlParam[3].ParameterName + ", " +
                        "etd = " + sqlParam[4].ParameterName + ", " +
                        "tgl_closing = " + sqlParam[5].ParameterName + ", " +
                        "voy = " + sqlParam[6].ParameterName + ", " +
                        "ro = " + sqlParam[7].ParameterName + ", " +
                        "deleted = " + sqlParam[8].ParameterName + ", " +
                        "keterangan = " + sqlParam[9].ParameterName + " " +
                        "WHERE schedule_id = " + sqlParam[10].ParameterName, con))
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
            }
            catch (Exception e)
            {
                if (sqlTransaction != null)
                    sqlTransaction.Rollback();

                Logging.Error("SqlScheduleRepository.cs - EditSchedule() " + e.Message);
            }
            finally
            {
                sqlTransaction.Dispose();
            }

            return n > 0;
        }

    }
}
