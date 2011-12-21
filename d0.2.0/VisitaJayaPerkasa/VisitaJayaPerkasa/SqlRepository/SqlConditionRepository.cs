using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlConditionRepository
    {
        public List<Condition> GetCondition()
        {
            List<Condition> listCondition = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT condition_id, condition_code, condition_name FROM [Condition] " +
                        "WHERE (deleted is null OR deleted = '0') " +
                        "ORDER BY condition_name ASC"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Condition condition = new Condition();
                            condition.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            condition.ConditionCode = reader.GetString(1);
                            condition.conditionName = reader.GetString(2);

                            if (listCondition == null)
                                listCondition = new List<Condition>();

                            listCondition.Add(condition);
                            condition = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlConditionRepository.cs - GetCondition() " + e.Message);
            }

            return listCondition;
        }
    }
}
