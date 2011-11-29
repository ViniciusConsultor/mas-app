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
            SqlParameter[] sqlParam = SqlUtility.SetSqlParamter(customer);
            bool isSuccess = SqlUtility.CreateNewRecord(_mainConnectionString, sql, sqlParam);

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

            return listCustomer;
        }

    }
}
