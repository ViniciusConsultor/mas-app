using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Web.Utility;
using Shipping.Business.Entities;
using System.Data.SqlClient;
using System.Data.Linq;

namespace Shipping.Data.Sql
{
    public class SqlConditionRepository : IConditionRepository
    {
        private readonly string _mainConnectionString;
        public SqlConditionRepository(string mainConnectionString) {
            _mainConnectionString = mainConnectionString;
        }

        public bool CreateCondition(Condition condition)
        {
            String sql = "Insert into Condition values(" + Utility.GetStringParameter(condition) + ")";
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(condition);
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            if (isSuccess)
                return true;

            return false;
        }

        public bool EditCondition(Condition condition)
        {
            string strParameter = Utility.GetStringParameter(condition);
            string[] strParameters = Utility.SplitParameter(strParameter);

            String sql = "Update Condition set condition_code =" + strParameters[0] +
                            ", condition_name =" + strParameters[1] +
                            " where condition_code =" + strParameters[0];
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(condition);
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            strParameters = null;
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }

        public bool DeleteCondition(string conditionCode)
        {
            string sql = "Delete Condition where condition_code = @ConditionCode";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("ConditionCode", conditionCode);

            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }

        public IEnumerable<Condition> GetListCondition()
        {
            DataContext dc = new DataContext(_mainConnectionString);
            IEnumerable<Condition> listCondition = null;

            try
            {
                listCondition = dc.GetTable<Condition>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlConditionRepository.cs - GetListCondition() " + e.InnerException);
            }

            dc = null;
            return listCondition;
        }

        public Condition GetConditionByID(string conditionCode)
        {
            DataContext dc = new DataContext(_mainConnectionString);
            Condition tempCondition = null;

            try
            {
                tempCondition = dc.GetTable<Condition>().Where(
                    x => x.ConditionCode == conditionCode
                        ).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlCategoryRepository.cs - GetCategoryByID " + e.InnerException);
            }

            dc = null;
            return tempCondition;
        }
    }
}
