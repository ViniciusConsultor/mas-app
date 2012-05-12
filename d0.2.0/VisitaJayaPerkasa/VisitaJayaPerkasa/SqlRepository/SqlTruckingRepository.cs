using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlTruckingRepository
    {
        public List<Trucking> ListTrucking()
        {
            List<Trucking> listTrucking = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT st.supplier_trucking_id, st.supplier_id, st.trucking_no " +
                        "FROM supplier_trucking st "
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Trucking trucking = new Trucking();
                            trucking.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            trucking.SupplierID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            trucking.TruckNo = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);

                            if (listTrucking == null)
                                listTrucking = new List<Trucking>();

                            listTrucking.Add(trucking);
                            trucking = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlTruckingRepository.cs - ListTrucking() " + e.Message);
            }

            return listTrucking;
        }

        public List<Trucking> ListTruckingBySchedule(Guid scheduleId)
        {
            List<Trucking> listTrucking = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT st.supplier_trucking_id, st.supplier_id, st.trucking_no " +
                        "FROM supplier_trucking st " +
                        "INNER JOIN pelayaran p ON p.supplier_id = st.supplier_id " +
                        "INNER JOIN pelayaran_detail pd ON pd.pelayaran_id = p.pelayaran_id " +
                        "INNER JOIN schedule sc ON sc.pelayaran_detail_id = pd.pelayaran_detail_id " +
                        "WHERE sc.schedule_id = '" + scheduleId.ToString() + "' "
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Trucking trucking = new Trucking();
                            trucking.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            trucking.SupplierID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            trucking.TruckNo = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);

                            if (listTrucking == null)
                                listTrucking = new List<Trucking>();

                            listTrucking.Add(trucking);
                            trucking = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlTruckingRepository.cs - ListTruckingBySchedule() " + e.Message);
            }

            return listTrucking;
        }
    }
}
