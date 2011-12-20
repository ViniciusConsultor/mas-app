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
        public List<Schedule> ListSchedule()
        {
            List<Schedule> listSchedule = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT s.schedule_id, s.berangkat, s.tujuan, s.pelayaran_id, s.vessel_id, s.voy, s.etd, " +
                        "s.keterangan, s.RO, s.tgl_closing, p.pelayaran_name, v.vessel_name FROM [Schedule] s JOIN " + 
                        "[Vessel] v on s.vessel_id = s.vessel_id AND (s.deleted is null OR s.deleted = '0') AND (v.deleted is null OR v.deleted = '0') JOIN " + 
                        "[Pelayaran] p on p.pelayaran_id = s.pelayaran_id AND (p.deleted is null OR p.deleted = '0')"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Schedule schedule = new Schedule();
                            schedule.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            schedule.berangkat = (Utility.Utility.IsDBNull(reader.GetValue(1))) ? null : reader.GetString(1);
                            schedule.tujuan = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);
                            schedule.berangkatTujuan = schedule.GetBerangkatTujuan();

                            schedule.vesselID = Utility.Utility.ConvertToUUID(reader.GetValue(3).ToString());
                            schedule.voy = (Utility.Utility.IsDBNull(reader.GetValue(4))) ? null : reader.GetString(4);
                            schedule.etd = reader.GetDateTime(5);
                            schedule.keterangan = (Utility.Utility.IsDBNull(reader.GetValue(6))) ? null : reader.GetString(6);
                            schedule.ro = (Utility.Utility.IsDBNull(reader.GetValue(7))) ? null : reader.GetString(7);
                            schedule.tglclosing = reader.GetDateTime(8);
                            schedule.namaPelayaran = reader.GetString(9);
                            schedule.namaKapal = reader.GetString(10);

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
    }
}
