using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Business.Entities;
using System.Data.SqlClient;
using Shipping.Web.Utility;
using System.Data.Linq;

namespace Shipping.Data.Sql
{
    public class SqlCustomerRepository : ICustomerRepository
    {
        private readonly String _mainConnectionString;

        public SqlCustomerRepository(string mainConnectionString)
        {
            _mainConnectionString = mainConnectionString;
        }

        public bool CreateCustomer(Customer customer)
        {
            String sql = "Insert into Customer values(" + Utility.GetStringParameter(customer) + ")";
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(customer);
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            if (isSuccess)
                return true;

            return false;
        }


        public IEnumerable<Customer> GetListCustomer()
        {
            DataContext dc = new DataContext(_mainConnectionString);
            IEnumerable<Customer> listCustomer = null;

            try
            {
                listCustomer = dc.GetTable<Customer>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("SqlCustomerRepository.cs - GetListCustomer() " + e.InnerException);
            }

            dc = null;
            return listCustomer;
        }



        public Customer GetCustomerByID(Guid ID)
        {
            DataContext dc = new DataContext(_mainConnectionString);
            Customer tempCust = null;

            try
            {
                tempCust = dc.GetTable<Customer>().Where(
                    x => x.ID == ID
                        ).FirstOrDefault();
            }
            catch(Exception e) {
                Console.WriteLine("SqlCustomerRepository.cs - GetCustomerByID " + e.InnerException);
            }

            dc = null;
            return tempCust;
        }

        public bool DeleteCustomer(Guid ID) {
            string sql = "Delete Customer where customer_id = @ID";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("ID", ID);

            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }

        public bool EditCustomer(Customer customer)
        {
            string strParameter = Utility.GetStringParameter(customer);
            string[] strParameters = Utility.SplitParameter(strParameter);
            
            String sql = "Update Customer set customer_name =" + strParameters[1] + 
                            ", office =" + strParameters[2] + 
                            ", address =" + strParameters[3] + ", phone =" + strParameters[4] + 
                            ", fax =" + strParameters[5] + ", email =" + strParameters[6] + 
                            ", contact_person =" + strParameters[7] +  
                            " where customer_id =" + strParameters[0];
            SqlParameter[] sqlParam = Utility.SwapSqlParameterByCriteria(SqlUtility.SetSqlParameter(customer), "ID");
            bool isSuccess = SqlUtility.ExecuteNonQuery(_mainConnectionString, sql, sqlParam);

            strParameters = null;
            sqlParam = null;

            if (isSuccess)
                return true;

            return false;
        }
    }
}
