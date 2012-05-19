using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlPaymentRepository
    {
        public List<Payment> GetListPayment()
        {
            List<Payment> listPayment = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT Id, Name FROM [TypeOfPayment] "
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Payment payment = new Payment();
                            payment.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            payment.Name = reader.GetString(1);

                            if (listPayment == null)
                                listPayment = new List<Payment>();

                            listPayment.Add(payment);
                            payment = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlPaymentRepository.cs - GetListPayment() " + e.Message);
            }

            return listPayment;
        }
    }
}
