using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Web.Utility;
using System.Data.SqlClient;
using System.Data.Linq;

namespace Shipping.Data.Sql
{
    public class SqlTypeContRepository : ITypeContRepository
    {
        private readonly string _mainConnectionString;
        public SqlTypeContRepository(string mainConnectionString) {
            _mainConnectionString = mainConnectionString;
        }

        public bool CreateTypeCont(Business.Entities.TypeCont typeCont)
        {
            String sql = "Insert into Type_Cont values(" + Utility.GetStringParameter(typeCont) + ")";
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(typeCont);
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            if (isSuccess)
                return true;

            return false;
        }

        public bool EditTypeCont(Business.Entities.TypeCont typeCont)
        {
            string strParameter = Utility.GetStringParameter(typeCont);
            string[] strParameters = Utility.SplitParameter(strParameter);

            String sql = "Update Type_Cont set type_code =" + strParameters[0] +
                            ", type_name =" + strParameters[1] +
                            " where type_code =" + strParameters[0];
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(typeCont);
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            strParameters = null;
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }

        public bool DeleteTypeCont(string ID)
        {
            string sql = "Delete Type_Cont where type_code = @TypeCode";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("TypeCode", ID);

            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }

        public IEnumerable<Business.Entities.TypeCont> GetListTypeCont()
        {
            DataContext dc = new DataContext(_mainConnectionString);
            IEnumerable<Business.Entities.TypeCont> listTypeCont = null;

            try
            {
                listTypeCont = dc.GetTable<Business.Entities.TypeCont>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlTypeContRepository.cs - GetListTypeCont() " + e.InnerException);
            }

            dc = null;
            return listTypeCont;
        }

        public Business.Entities.TypeCont GetTypeContByID(string ID)
        {
            DataContext dc = new DataContext(_mainConnectionString);
            Business.Entities.TypeCont tempTypeCont = null;

            try
            {
                tempTypeCont = dc.GetTable<Business.Entities.TypeCont>().Where(
                    x => x.TypeCode == ID
                        ).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlLeadTimeRepository.cs - GetTypeContByID " + e.InnerException);
            }

            dc = null;
            return tempTypeCont;
        }
    }
}
