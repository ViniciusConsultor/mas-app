using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Web.Utility;
using System.Data.SqlClient;
using System.Data.Linq;

namespace Shipping.Data.Sql
{
    public class SqlLeadTimeRepository : ILeadTimeRepository
    {
        private readonly string _mainConnectionString;

        public SqlLeadTimeRepository(string mainConnectionString)
        {
            _mainConnectionString = mainConnectionString;
        }

        public bool CreateLeadTime(Business.Entities.LeadTime leadTime)
        {
            String sql = "Insert into Lead_Time values(" + Utility.GetStringParameter(leadTime) + ")";
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(leadTime);
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            if (isSuccess)
                return true;

            return false;
        }

        public bool EditLeadTime(Business.Entities.LeadTime leadTime)
        {
            string strParameter = Utility.GetStringParameter(leadTime);
            string[] strParameters = Utility.SplitParameter(strParameter);

            String sql = "Update Lead_Time set city_code =" + strParameters[0] +
                            ", days =" + strParameters[1] +
                            " where city_code =" + strParameters[0];
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(leadTime);
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            strParameters = null;
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }

        public bool DeleteLeadTime(string ID)
        {
            string sql = "Delete Lead_Time where city_code = @CityCode";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("CityCode", ID);

            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }

        public IEnumerable<Business.Entities.LeadTime> GetListLeadTime()
        {
            DataContext dc = new DataContext(_mainConnectionString);
            IEnumerable<Business.Entities.LeadTime> listLeadTime = null;

            try
            {
                listLeadTime = dc.GetTable<Business.Entities.LeadTime>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlLeadTimeRepository.cs - GetListLeadTime() " + e.InnerException);
            }

            dc = null;
            return listLeadTime;
        }

        public Business.Entities.LeadTime GetLeadTimeByID(string ID)
        {
            DataContext dc = new DataContext(_mainConnectionString);
            Business.Entities.LeadTime tempLeadTime = null;

            try
            {
                tempLeadTime = dc.GetTable<Business.Entities.LeadTime>().Where(
                    x => x.CityCode == ID
                        ).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlLeadTimeRepository.cs - GetLeadTimeByID " + e.InnerException);
            }

            dc = null;
            return tempLeadTime;
        }
    }
}
