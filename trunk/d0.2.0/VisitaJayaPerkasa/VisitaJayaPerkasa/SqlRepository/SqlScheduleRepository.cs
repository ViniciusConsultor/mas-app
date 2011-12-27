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
                        "SELECT s.schedule_id, s.berangkat, s.tujuan, s.pelayaran_id, s.vessel_id, s.voy, s.etd, " +
                        "s.keterangan, s.RO, s.tgl_closing, p.name, v.vessel_name, " +
                        "(SELECT TOP 1 city_name FROM [CITY] cc WHERE cc.city_id = s.berangkat) as berangkats, " + 
                        "(SELECT TOP 1 city_name FROM [CITY] cc WHERE cc.city_id = s.tujuan) as tujuans " +
                        "FROM [Schedule] s JOIN " +
                        "[Vessel] v on v.vessel_id like '%" + vessel + "%' AND v.vessel_id = s.vessel_id AND (s.etd > '" + beginDate + "' AND s.etd < '" + endDate + "') AND " + 
                        "s.voy like '%" + voy + "%' AND " + 
                        "s.tujuan like '%" + destination + "%' AND s.vessel_id = s.vessel_id AND (s.deleted is null OR s.deleted = '0') AND (v.deleted is null OR v.deleted = '0') JOIN " + 
                        "[Pelayaran] p on p.pelayaran_id = s.pelayaran_id AND (p.deleted is null OR p.deleted = '0')"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Schedule schedule = new Schedule();
                            schedule.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            schedule.berangkat = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString()); 
                            schedule.tujuan = Utility.Utility.ConvertToUUID(reader.GetValue(2).ToString());
                            schedule.pelayaranID = Utility.Utility.ConvertToUUID(reader.GetValue(3).ToString());
                            schedule.vesselID = Utility.Utility.ConvertToUUID(reader.GetValue(4).ToString());
                            schedule.voy = (Utility.Utility.IsDBNull(reader.GetValue(5))) ? null : reader.GetString(5);
                            schedule.etd = reader.GetDateTime(6);
                            schedule.keterangan = (Utility.Utility.IsDBNull(reader.GetValue(7))) ? null : reader.GetString(7);
                            schedule.ro = (Utility.Utility.IsDBNull(reader.GetValue(8))) ? null : reader.GetString(8);
                            schedule.tglclosing = reader.GetDateTime(9);
                            schedule.namaPelayaran = reader.GetString(10);
                            schedule.namaKapal = reader.GetString(11);
                            schedule.berangkatTujuan = reader.GetString(12) + " - " + reader.GetString(13);


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
                        sqlParam[10].ParameterName +
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
