using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlRecipientRepository
    {
        public List<Recipient> GetRecipient(){
            List<Recipient> listRecipient = null;

                try
                {
                    using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                    {
                        con.Open();

                        using (SqlCommand command = new SqlCommand(
                            "SELECT * FROM [Recipient] r " +
                            "WHERE (r.deleted is null OR r.deleted = '0') " +  
                            "ORDER BY recipient_name ASC"
                            , con))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Recipient recipient= new Recipient();
                                recipient.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                                recipient.Name = reader.GetString(1);

                                if (listRecipient == null)
                                    listRecipient = new List<Recipient>();

                                listRecipient.Add(recipient);
                                recipient = null;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logging.Error("SqlRecipientRepository.cs - GetRecipient() " + e.Message);
                }

                return listRecipient;
        }
    }
}
