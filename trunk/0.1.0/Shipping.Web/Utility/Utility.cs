using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;

namespace Shipping.Web.Utility
{
    public static class Utility
    {
        public static Guid NewUUID() {
            return Guid.NewGuid();
        }

        public static string GetStringParameter(object par1)
        {
            PropertyInfo[] fields = par1.GetType().GetProperties(); 
            string result = "";

            foreach (var field in fields)
            {
                string name = field.Name;
                result += "@" + name + ", ";
            }

            result = result.Substring(0, result.Length - 2);
            return result;
        }

        /**swap index 
         * ex.
         * [1,2,3,4] swap by 3
         * so that will be like [1,2,4,3]
         * */
        public static SqlParameter[] SwapSqlParameterByCriteria(SqlParameter[] sqlParam, string criteriaSwap) {
            SqlParameter[] tempObj = new SqlParameter[sqlParam.Length];

            for (int i = 0; i < sqlParam.Length; i++)
            {
                tempObj[i] = sqlParam[i];

                if (sqlParam[i].ParameterName.Equals(criteriaSwap))
                {
                    SqlParameter tempSqlParameter = sqlParam[i];

                    for (int j = i+1; j < sqlParam.Length; j++)
                        tempObj[j-1] = sqlParam[j];

                    tempObj[sqlParam.Length - 1] = tempSqlParameter;
                    tempSqlParameter = null;
                    break;
                }
            }

            sqlParam = null;
            return tempObj;
        }

        public static string[] SplitParameter(string strParameter) {
            return strParameter.Split(',');
        }

        public static bool IsStringNullOrEmpty(object str) {
            return false;

/*            if (str == null)
                return true;
            else if (str.Trim().Length == 0)
                return true;
            else
                return false;*/
        }
    }
}
