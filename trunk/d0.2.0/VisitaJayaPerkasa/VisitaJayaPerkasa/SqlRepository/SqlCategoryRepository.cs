using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlCategoryRepository
    {
        public List<Category> GetCategories()
        {
            List<Category> listCategory = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT category_id, category_code, category_name FROM [Category] " +
                        "WHERE (deleted is null OR deleted = '0') " +
                        "ORDER BY category_name ASC"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Category category = new Category();
                            category.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            category.CategoryCode = reader.GetString(1);
                            category.CategoryName = reader.GetString(2);

                            if (listCategory == null)
                                listCategory = new List<Category>();

                            listCategory.Add(category);
                            category = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCategoryRepository.cs - GetCategories() " + e.Message);
            }

            return listCategory;
        }
        public bool CheckCategoryCode(SqlParameter[] sqlParam, bool excludeDeleted = true)
        {
            bool exists = false;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    string conditional = "";
                    if (!excludeDeleted)
                        conditional = " AND deleted = '1'";

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 category_id FROM [Category] WHERE category_code = " + sqlParam[0].ParameterName + conditional, con))
                    {
                        foreach (SqlParameter tempSqlParam in sqlParam)
                            command.Parameters.Add(tempSqlParam);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            exists = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Logging.Error("SqlCategoryRepository.cs - CheckCategoryCode() " + e.Message);
            }

            return exists;
        }

        public bool CreateCategory(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            try
            {
                using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(
                        "Insert into [Category] values (" +
                        sqlParam[0].ParameterName + ", " +
                        sqlParam[1].ParameterName + ", " +
                        sqlParam[2].ParameterName + ", " +
                        sqlParam[3].ParameterName +
                        ")", con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 0; i < sqlParam.Length; i++)
                            command.Parameters.Add(sqlParam[i]);
                        n = command.ExecuteNonQuery();
                    }

                    if (n > 0)
                        sqlTransaction.Commit();
                    else
                        sqlTransaction.Rollback();
                }
            }
            catch (Exception e)
            {
                if (sqlTransaction != null)
                    sqlTransaction.Rollback();

                Logging.Error("SqlCategoryRepository.cs - CreateCategory() " + e.Message);
            }
            finally
            {
                sqlTransaction.Dispose();
            }

            return n > 0;
        }

        public bool EditCategory(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            try
            {
                using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(
                        "Update [Category] set category_code = " + sqlParam[0].ParameterName +
                        ", category_name = " + sqlParam[1].ParameterName +
                        " WHERE category_id = " + sqlParam[2].ParameterName, con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 0; i < sqlParam.Length; i++)
                            command.Parameters.Add(sqlParam[i]);
                        n = command.ExecuteNonQuery();
                    }

                    if (n > 0)
                        sqlTransaction.Commit();
                    else
                        sqlTransaction.Rollback();
                }
            }
            catch (Exception e)
            {
                if (sqlTransaction != null)
                    sqlTransaction.Rollback();

                Logging.Error("SqlCategoryRepository.cs - EditCategory() " + e.Message);
            }
            finally
            {
                sqlTransaction.Dispose();
            }

            return n > 0;
        }

        public bool DeleteCategory(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            try
            {
                using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(
                        "Update [Category] set deleted = '1' WHERE category_id = " + sqlParam[0].ParameterName, con))
                    {
                        command.Transaction = sqlTransaction;
                        command.Parameters.Add(sqlParam[0]);
                        n = command.ExecuteNonQuery();
                    }

                    if (n > 0)
                        sqlTransaction.Commit();
                    else
                        sqlTransaction.Rollback();
                }
            }
            catch (Exception e)
            {
                if (sqlTransaction != null)
                    sqlTransaction.Rollback();

                Logging.Error("SqlCategoryRepository.cs - DeleteCategory() " + e.Message);
            }
            finally
            {
                sqlTransaction.Dispose();
            }

            return n > 0;
        }
    }
}
