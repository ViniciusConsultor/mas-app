using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Logging;
using Shipping.Business.Entities;
using Shipping.Business.Entities.Collections;
using System.Data.Linq;
using Shipping.Web.Utility;
using System.Data.SqlClient;

namespace Shipping.Data.Sql
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private string _mainConnectionString;
        private Dictionary<string, string> _satelliteConnectionStrings;
        private ILogger _logger;

        public SqlCategoryRepository(string mainConnectionString, Dictionary<string, string> satelliteConnectionStrings, ILogger logger)
        {
            _mainConnectionString = mainConnectionString;
            _satelliteConnectionStrings = satelliteConnectionStrings;
            _logger = logger;
        }


        public List<Category> GetCategories()
        {
            _logger.Debug(this, "Getting list of all Category");
            return SqlUtility.ExecuteXmlStoredProcedure<CategoryCollection>(_mainConnectionString, _logger, "Category_GetCategories");
        }

        public bool CreateCategory(Category category)
        {
            String sql = "Insert into Category values(" + Utility.GetStringParameter(category) + ")";
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(category);
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            if (isSuccess)
                return true;

            return false;
        }


        public bool EditCategory(Category category)
        {
            string strParameter = Utility.GetStringParameter(category);
            string[] strParameters = Utility.SplitParameter(strParameter);

            String sql = "Update Category set category_code =" + strParameters[0] +
                            ", category_name =" + strParameters[1] +
                            " where category_code =" + strParameters[0];
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(category);
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            strParameters = null;
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }

        public bool DeleteCategory(string categoryCode)
        {
            string sql = "Delete Category where category_code = @CategoryCode";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("CategoryCode", categoryCode);

            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }

        public IEnumerable<Category> GetCategory()
        {
            DataContext dc = new DataContext(_mainConnectionString);
            IEnumerable<Category> listCategory = null;

            try
            {
                listCategory = dc.GetTable<Category>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlCategoryRepository.cs - GetCategory() " + e.InnerException);
            }

            dc = null;
            return listCategory;
        }

        public Category GetCategoryByID(string categoryCode)
        {
            DataContext dc = new DataContext(_mainConnectionString);
            Category tempCategory = null;

            try
            {
                tempCategory = dc.GetTable<Category>().Where(
                    x => x.CategoryCode == categoryCode
                        ).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlCategoryRepository.cs - GetCategoryByID " + e.InnerException);
            }

            dc = null;
            return tempCategory;
        }
    }
}
