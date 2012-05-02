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
                        "SELECT st.supplier_trucking_id, st.supplier_id, st.trucking_no" +
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
                Logging.Error("SqlScheduleRepository.cs - ListSchedule() " + e.Message);
            }

            return listTrucking;
        }
    }
}
