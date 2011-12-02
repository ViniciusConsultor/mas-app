using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Web.Utility;
using System.Data.SqlClient;
using System.Data.Linq;
using Shipping.Business.Entities;

namespace Shipping.Data.Sql
{
    public class SqlRoleRepository : IRoleRepository
    {
        private readonly string _mainConnectionString;
        public SqlRoleRepository(string mainConnectionString) {
            _mainConnectionString = mainConnectionString;
        }

        public bool CreateRole(Business.Entities.Role role)
        {
            String sql = "Insert into Role values(" + Utility.GetStringParameter(role) + ")";
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(role);
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            if (isSuccess)
                return true;

            return false;
        }

        public bool EditRole(Business.Entities.Role role)
        {
            string strParameter = Utility.GetStringParameter(role);
            string[] strParameters = Utility.SplitParameter(strParameter);

            String sql = "Update Role set name =" + strParameters[0] +
                            ", description =" + strParameters[1] +
                            " where name =" + strParameters[0];
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(role);
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            strParameters = null;
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }

        public bool DeleteRole(string ID)
        {
            string sql = "Delete Role where name = @Name";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("Name", ID);

            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }

        public IEnumerable<Business.Entities.Role> GetListRole()
        {
            DataContext dc = new DataContext(_mainConnectionString);
            IEnumerable<Role> listRole = null;

            try
            {
                listRole = dc.GetTable<Role>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlRoleRepository.cs - GetRole() " + e.InnerException);
            }

            dc = null;
            return listRole;
        }

        public Business.Entities.Role GetRoleByID(string ID)
        {
            DataContext dc = new DataContext(_mainConnectionString);
            Role tempRole = null;

            try
            {
                tempRole = dc.GetTable<Role>().Where(
                    x => x.Name == ID
                        ).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlRoleRepository.cs - GetRoleByID " + e.InnerException);
            }

            dc = null;
            return tempRole;
        }
    }
}
