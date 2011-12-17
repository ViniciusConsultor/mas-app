using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlUtility
    {
        public static SqlParameter[] SetSqlParameter(string[] key, object[] value) { 
            
            SqlParameter[] sqlParam = new SqlParameter[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                    sqlParam[i] = new SqlParameter();
                    sqlParam[i].ParameterName = "@" + key[i];

                    if (value[i] == null)
                        sqlParam[i].Value = DBNull.Value;
                    else
                        sqlParam[i].Value = (value[i].Equals("")) ? DBNull.Value : value[i];
            }
            return sqlParam;
        }

        public static object isDBNULL(string text) {
            if (text.Trim().Length == 0)
                return DBNull.Value;
            else
                return text;
        }
    }
}
