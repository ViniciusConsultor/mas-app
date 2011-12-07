using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Data;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository) {
            _cityRepository = cityRepository;
        }

        public void SaveCity(City city)
        {
            _cityRepository.SaveCity(city);
        }

        public void DeleteCity(string cityCode)
        {
            _cityRepository.DeleteCity(cityCode);
        }

        public IEnumerable<City> GetListCity()
        {
            return _cityRepository.GetListCity();
        }

        public City GetCityByID(string cityCode)
        {
            return _cityRepository.GetCityByID(cityCode);
        }


        public IEnumerable<City> GetCityBySearch(string searchWord)
        {
            return _cityRepository.GetCityBySearch(searchWord);
        }
    }
}
