using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Data;

namespace VisitaJayaPerkasa.Data.Sql
{
    public class CityRepository : ICityRepository
    {
        private readonly string _mainConnectionString;

        public CityRepository(string mainConnectionString) {
            _mainConnectionString = mainConnectionString;
        }

        public void SaveCity(City city)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            using (var scope = repo.GetTransaction())
            {
                if (GetCityByID(city.CityCode) == null)
                {
                    //Create new
                    repo.Insert(city);
                }
                else
                {
                    //Update it

                    repo.Update("CITY", "city_code", city);
                }

                scope.Complete();
            }

            repo.CloseSharedConnection();
        }

        public void DeleteCity(string cityCode)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();
            City city = GetCityByID(cityCode);
            repo.Delete("CITY", "city_code", city);
            repo.CloseSharedConnection();
        }

        public IEnumerable<City> GetListCity()
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            List<City> cities = repo.Fetch<City>("SELECT * FROM [CITY]").ToList<City>();

            repo.CloseSharedConnection();

            return cities;
        }

        public City GetCityByID(string cityCode)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            City cities = repo.SingleOrDefault<City>("SELECT * FROM CITY WHERE city_code=@0", cityCode);

            repo.CloseSharedConnection();

            return cities;
        }
    }
}
