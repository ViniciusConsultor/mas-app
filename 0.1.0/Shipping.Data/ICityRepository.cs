using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Business.Entities;

namespace Shipping.Data
{
    public interface ICityRepository
    {
        bool CreateCity(City city);
        bool EditCity(City city);
        bool DeleteCity(string cityCode);
        IEnumerable<City> GetListCity();
        City GetCityByID(string cityCode);
    }
}
