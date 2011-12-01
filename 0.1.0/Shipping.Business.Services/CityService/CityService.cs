using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Data;
using Shipping.Business.Entities;

namespace Shipping.Business.Services.CityService
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository) {
            _cityRepository = cityRepository;
        }

        public bool CreateCity(City city)
        {
            return _cityRepository.CreateCity(city);
        }

        public bool EditCity(City city)
        {
            return _cityRepository.EditCity(city);
        }

        public bool DeleteCity(string cityCode)
        {
            return _cityRepository.DeleteCity(cityCode);
        }

        public IEnumerable<City> GetListCity()
        {
            return _cityRepository.GetListCity();
        }

        public City GetCityByID(string cityCode)
        {
            return _cityRepository.GetCityByID(cityCode);
        }
    }
}
