using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlCityRepository
    {
        public List<City> GetCity()
        {
            List<City> listCity = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT city_id, city_code, city_name FROM [City] " +
                        "WHERE (deleted is null OR deleted = '0') " +
                        "ORDER BY city_name ASC"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            City city = new City();
                            city.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            city.CityCode = reader.GetString(1);
                            city.CityName = reader.GetString(2);

                            if (listCity == null)
                                listCity = new List<City>();

                            listCity.Add(city);
                            city = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCityRepository.cs - GetCity() " + e.Message);
            }

            return listCity;
        }
    }
}
