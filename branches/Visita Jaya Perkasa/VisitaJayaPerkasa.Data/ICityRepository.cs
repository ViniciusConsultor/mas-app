using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data
{
    public interface ICityRepository
    {
        void SaveCity(City city);
        void DeleteCity(string cityCode);
        IEnumerable<City> GetListCity();
        City GetCityByID(string cityCode);
    }
}
