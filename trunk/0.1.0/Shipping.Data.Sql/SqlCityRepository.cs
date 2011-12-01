using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Shipping.Web.Utility;
using System.Data.Linq;
using Shipping.Business.Entities;

namespace Shipping.Data.Sql
{
    public class SqlCityRepository : ICityRepository
    {
        private readonly string _mainConnectionString;

        public SqlCityRepository(string mainConnectionString) {
            _mainConnectionString = mainConnectionString;
        }

        public bool CreateCity(City city)
        {
            String sql = "Insert into City values(" + Utility.GetStringParameter(city) + ")";
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(city);
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            if (isSuccess)
                return true;

            return false;
        }

        public bool EditCity(City city)
        {
            string strParameter = Utility.GetStringParameter(city);
            string[] strParameters = Utility.SplitParameter(strParameter);

            String sql = "Update City set city_code =" + strParameters[0] +
                            ", city_name =" + strParameters[1] +
                            " where city_code =" + strParameters[0];
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(city);
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            strParameters = null;
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }

        public bool DeleteCity(string cityCode)
        {
            string sql = "Delete City where city_code = @CityCode";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("CityCode", cityCode);

            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }

        public IEnumerable<City> GetListCity()
        {
            DataContext dc = new DataContext(_mainConnectionString);
            IEnumerable<City> listCategory = null;

            try
            {
                listCategory = dc.GetTable<City>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlCategoryRepository.cs - GetCategory() " + e.InnerException);
            }

            dc = null;
            return listCategory;
        }

        public City GetCityByID(string cityCode)
        {
            DataContext dc = new DataContext(_mainConnectionString);
            City tempCity = null;

            try
            {
                tempCity = dc.GetTable<City>().Where(
                    x => x.CityCode == cityCode
                        ).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlCityRepository.cs - GetCityByID " + e.InnerException);
            }

            dc = null;
            return tempCity;
        }
    }
}
