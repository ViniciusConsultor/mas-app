using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public interface ICityService
    {
        void SaveCity(City city);
        void DeleteCity(string cityCode);
        IEnumerable<City> GetListCity();
        City GetCityByID(string cityCode);
        IEnumerable<City> GetCityBySearch(string searchWord);
    }
}
