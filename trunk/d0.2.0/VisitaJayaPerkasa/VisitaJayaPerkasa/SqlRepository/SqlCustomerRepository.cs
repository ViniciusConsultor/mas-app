using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlCustomerRepository
    {
        public List<Customer> ListCustomers()
        {
            List<Customer> listCustomer = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT * FROM [Customer] WHERE deleted is null OR deleted = '0'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Customer customer = new Customer();
                            customer.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            customer.CustomerName = (Utility.Utility.IsDBNull(reader.GetValue(1))) ? null : reader.GetString(1);
                            customer.Office = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);
                            customer.Address = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);
                            customer.Phone = (Utility.Utility.IsDBNull(reader.GetValue(4))) ? null : reader.GetString(4);
                            customer.Fax = (Utility.Utility.IsDBNull(reader.GetValue(5))) ? null : reader.GetString(5);
                            customer.Email = (Utility.Utility.IsDBNull(reader.GetValue(6))) ? null : reader.GetString(6);
                            customer.ContactPerson = (Utility.Utility.IsDBNull(reader.GetValue(7))) ? null : reader.GetString(7);

                            if (listCustomer == null)
                                listCustomer = new List<Customer>();

                            listCustomer.Add(customer);
                            customer = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerRepository.cs - listCustomers() " + e.Message);
            }

            return listCustomer;
        }


        public List<CustomerDetail> ListCustomerDetail(Guid ID)
        {
            List<CustomerDetail> listCustomerDetail = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT * FROM [Customer_Detail] WHERE customer_id = '" + ID + "' AND (deleted is null OR deleted = '0')"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CustomerDetail customer = new CustomerDetail();
                            customer.CustomerDetailID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            customer.ID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            customer.FirstName = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);
                            customer.LastName = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);
                            customer.CustomerDetailAddress = (Utility.Utility.IsDBNull(reader.GetValue(4))) ? null : reader.GetString(4);
                            customer.CustomerDetailPhone = (Utility.Utility.IsDBNull(reader.GetValue(5))) ? null : reader.GetString(5);
                            customer.CustomerDetailMobilePhone = (Utility.Utility.IsDBNull(reader.GetValue(6))) ? null : reader.GetString(6);

                            if (listCustomerDetail == null)
                                listCustomerDetail = new List<CustomerDetail>();

                            listCustomerDetail.Add(customer);
                            customer = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerRepository.cs - listCustomerDetail() " + e.Message);
            }

            return listCustomerDetail;
        }

        public bool DeleteCustomer(SqlParameter[] sqlParam)
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

                    SqlParameter tempSqlParam = sqlParam[0];
                    using (SqlCommand command = new SqlCommand(
                        "Update [Customer_Detail] set deleted = '1' WHERE customer_id = " + sqlParam[0].ParameterName, con))
                    {
                        command.Transaction = sqlTransaction;
                        command.Parameters.Add(sqlParam[0]);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();

                            using (SqlCommand subCommand = new SqlCommand(
                                "Update [Customer] set deleted = '1' WHERE customer_id = " + sqlParam[0].ParameterName, con
                                )) {
                                    subCommand.Transaction = sqlTransaction;
                                    subCommand.Parameters.Add(sqlParam[0]);
                                    n = subCommand.ExecuteNonQuery();
                            }
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

                Logging.Error("SqlCustomerRepository.cs - DeleteCustomer() " + e.Message);
            }
            finally
            {
                sqlTransaction.Dispose();
            }

            return n > 0;
        }
    }
}
